using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StepITFinalProject.Attributes;
using StepITFinalProject.Common;
using StepITFinalProject.Enums;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;
using StepITFinalProject.Services.Interfaces;

namespace StepITFinalProject.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    //    [AuthorizedRoles(ROLES.READER, ROLES.LIBRARIAN)]
    public ActionResult<ApiResponse<List<GetBookResponse>>> GetAllBooks([FromQuery] string? search, [FromQuery] int? authorId, [FromQuery] int? genreId, [FromQuery] bool availableOnly = false)
    {
        var response = _bookService.GetAllBooks(search, authorId, genreId, availableOnly);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id:int}")]
  //  [AuthorizedRoles(ROLES.READER, ROLES.LIBRARIAN)]
    public ActionResult<ApiResponse<GetBookDetailsResponse>> GetBookById(int id)
    {
        var response = _bookService.GetBookById(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    //    [AuthorizedRoles(ROLES.LIBRARIAN)]
    public ActionResult<ApiResponse<CreateBookResponse>> CreateBook(CreateBookRequest request)
    {
        var response = _bookService.CreateBook(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id:int}")]
    //    [AuthorizedRoles(ROLES.LIBRARIAN)]
    public ActionResult<ApiResponse<GetBookDetailsResponse>> UpdateBook(int id, UpdateBookRequest request)
    {
        var response = _bookService.UpdateBook(id, request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id:int}")]
    //    [AuthorizedRoles(ROLES.LIBRARIAN)]
    public ActionResult<ApiResponse<bool>> DeleteBook(int id)
    {
        var response = _bookService.DeleteBook(id);

        if (response.Success)
        {
            return NoContent();   // 204 must have an empty body
        }

        return StatusCode(response.StatusCode, response);
    }
}
