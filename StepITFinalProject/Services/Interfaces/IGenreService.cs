using StepITFinalProject.Common;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;


namespace StepITFinalProject.Services.Interfaces;

public interface IGenreService
{
    ApiResponse<List<GetGenreResponse>> GetAllGenres();
    ApiResponse<CreateGenreResponse> CreateGenre(CreateGenreRequest request);
}
