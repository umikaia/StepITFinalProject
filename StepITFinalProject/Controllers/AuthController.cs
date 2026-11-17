using Microsoft.AspNetCore.Mvc;
using StepITFinalProject.Common;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;
using StepITFinalProject.Services.Interfaces;
namespace StepITFinalProject.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public ActionResult<ApiResponse<AuthResponse>> Register(RegisterRequest request)
    {
        var response = _authService.Register(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("login")]
    public ActionResult<ApiResponse<AuthResponse>> Login(LoginRequest request)
    {
        var response = _authService.Login(request);
        return StatusCode(response.StatusCode, response);
    }
}