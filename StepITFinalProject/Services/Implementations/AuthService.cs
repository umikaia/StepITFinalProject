using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StepITFinalProject.Common;
using StepITFinalProject.Data;
using StepITFinalProject.Models;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;
using StepITFinalProject.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace StepITFinalProject.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly Baza _baza;
    private readonly JwtSettings _jwtSettings;
    public AuthService(Baza baza, IOptions<JwtSettings> jwtSettings)
    {
        _baza = baza;
        _jwtSettings = jwtSettings.Value;
    }

    public ApiResponse<AuthResponse> Register(RegisterRequest request)
    {
        bool emailTaken = _baza.Users.Any(u => u.Email.ToLower() == request.Email.ToLower());

        if (emailTaken)
        {
            return ApiResponse<AuthResponse>.Fail(409, "Email is already taken.");
        }

        var user = new User
        {
            Email = request.Email,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _baza.Users.Add(user);
        _baza.SaveChanges();

        var response = new AuthResponse
        {
            Token = GenerateToken(user)
        };

        return ApiResponse<AuthResponse>.OK(response);
    }

    public ApiResponse<AuthResponse> Login(LoginRequest request)
    {
        var user = _baza.Users.FirstOrDefault(u => u.Email.ToLower() == request.Email.ToLower());
        if (user == null)
        {
            return ApiResponse<AuthResponse>.Fail(401, "Invalid email or password");
        }
        bool passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword);
        if (!passwordValid)
        {
            return ApiResponse<AuthResponse>.Fail(401, "Invalid email or password");
        }

        var response = new AuthResponse
        {
            Token = GenerateToken(user)
        };
        return ApiResponse<AuthResponse>.OK(response);
    }



    //public ApiResponse<bool> MakeSuperAdmin(string email)
    //{
    //    var user = _baza.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
    //    if (user == null)
    //    {
    //        return ApiResponse<bool>.Fail(401, "User not found");

    //    }

    //    user.Role = Enums.ROLES.SUPER_ADMIN;
    //    _baza.Users.Update(user);
    //    _baza.SaveChanges();

    //    return ApiResponse<bool>.OK(true, "User promoted to SUPER_ADMIN successfully");
    //}


    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
