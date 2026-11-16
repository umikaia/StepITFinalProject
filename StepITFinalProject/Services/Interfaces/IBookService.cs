using StepITFinalProject.Common;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;
namespace StepITFinalProject.Services.Interfaces;

public interface IBookService
{
    ApiResponse<List<GetBookResponse>> GetAllBooks(string? search, int? authorId, int? genreId, bool onlyAvailable);
    ApiResponse<GetBookDetailsResponse> GetBookById(int id);
    ApiResponse<CreateBookResponse> CreateBook(CreateBookRequest request);
    ApiResponse<GetBookDetailsResponse> UpdateBook(int id, UpdateBookRequest request);
    ApiResponse<bool> DeleteBook(int id);
}

