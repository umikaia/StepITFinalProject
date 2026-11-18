using StepITFinalProject.Common;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;

namespace StepITFinalProject.Services.Interfaces;

public interface IBorrowService
{
    ApiResponse<GetBorrowResponse> BorrowBook(int userId, BorrowRequest request);
    ApiResponse<GetBorrowResponse> ReturnBook(int userId, int borrowId);
    ApiResponse<List<GetBorrowResponse>> GetMyBorrows(int userId, bool activeOnly);
    ApiResponse<List<GetBorrowResponse>> GetAllBorrows(bool overdueOnly, int? userId, bool active);
}
