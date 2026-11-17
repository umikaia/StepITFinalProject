using StepITFinalProject.Common;
using StepITFinalProject.Responses;
namespace StepITFinalProject.Services.Interfaces;

public interface ILibraryCardService
{
    ApiResponse<GetLibraryCardResponse> CreateMyCard(int userId);
    ApiResponse<GetLibraryCardResponse> GetMyCard(int userId);
}