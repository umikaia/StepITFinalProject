using StepITFinalProject.Models;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;
using StepITFinalProject.Common;


namespace StepITFinalProject.Services.Interfaces;

public interface IAuthorService
{
    ApiResponse<List<GetAuthorResponse>> GetAllAuthors();
    ApiResponse<GetAuthorDetailsResponse> GetAuthorByIdWithBooks(int id);
    ApiResponse<CreateAuthorResponse> CreateAuthor(CreateAuthorRequest request);
    ApiResponse<GetAuthorResponse> UpdateAuthor(int id, UpdateAuthorRequest request);
    ApiResponse<bool> DeleteAuthor(int id);
}
