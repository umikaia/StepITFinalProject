using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StepITFinalProject.Attributes;
using StepITFinalProject.Common;
using StepITFinalProject.Enums;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;
using StepITFinalProject.Services.Interfaces;

namespace StepITFinalProject.Controllers;

[Route("api/borrows")]
[ApiController]
public class BorrowsController : ControllerBase
{

    private readonly IBorrowService _borrowService;
    public BorrowsController(IBorrowService borrowService)
    {
        _borrowService = borrowService;
    }

    [HttpPost]
    [AuthorizedRoles(ROLES.READER)]
    public ActionResult<ApiResponse<GetBorrowResponse>> BorrowBook(BorrowRequest request)
    {
        var response = _borrowService.BorrowBook(User.GetUserId(), request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("{id:int}/return")]
   // [AuthorizedRoles(ROLES.READER)]
    public ActionResult<ApiResponse<GetBorrowResponse>> ReturnBook(int id)
    {
        var response = _borrowService.ReturnBook(User.GetUserId(), id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("me")]
    //[AuthorizedRoles(ROLES.READER)]
    public ActionResult<ApiResponse<List<GetBorrowResponse>>> GetMyBorrows([FromQuery] bool activeOnly = false)
    {
        var response = _borrowService.GetMyBorrows(User.GetUserId(), activeOnly);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    //[AuthorizedRoles(ROLES.LIBRARIAN)]
    public ActionResult<ApiResponse<List<GetBorrowResponse>>> GetAllBorrows([FromQuery] bool overdueOnly = false, [FromQuery] int? userId = null)
    {
        var response = _borrowService.GetAllBorrows(overdueOnly, userId);
        return StatusCode(response.StatusCode, response);
    }
}
