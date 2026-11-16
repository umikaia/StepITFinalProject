using StepITFinalProject.Common;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;
namespace StepITFinalProject.Services.Interfaces;

public interface IAuthService
{
    ApiResponse<AuthResponse> Register(RegisterRequest request);
    ApiResponse<AuthResponse> Login(LoginRequest request);
}
