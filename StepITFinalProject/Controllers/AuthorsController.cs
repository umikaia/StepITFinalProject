using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepITFinalProject.Requests;
using StepITFinalProject.Services.Interfaces;
using StepITFinalProject.Responses;
using StepITFinalProject.Common;
using StepITFinalProject.Enums;
using StepITFinalProject.Attributes;
namespace StepITFinalProject.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;
    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

//    [AuthorizedRoles(ROLES.READER, ROLES.LIBRARIAN)]
    [HttpGet]
    public ActionResult<ApiResponse<List<GetAuthorResponse>>> GetAllAuthors()
    {
        var response = _authorService.GetAllAuthors();
        return StatusCode(response.StatusCode, response);
    }

  //  [AuthorizedRoles(ROLES.READER, ROLES.LIBRARIAN)]
    [HttpGet("/{id}")]
    public ActionResult<ApiResponse<GetAuthorDetailsResponse>> GetAuthorByIdWithBooks(int id)
    {
        var response = _authorService.GetAuthorByIdWithBooks(id);
        return StatusCode(response.StatusCode, response);
    }

    // [AuthorizedRoles(ROLES.LIBRARIAN)]
    [HttpPost]
    public ActionResult<ApiResponse<CreateAuthorResponse>> CreateAuthor(CreateAuthorRequest request)
    {
        var response = _authorService.CreateAuthor(request);
        return StatusCode(response.StatusCode, response);
    }

    //[AuthorizedRoles(ROLES.LIBRARIAN)]
    [HttpPut("/{id}")]
    public ActionResult<ApiResponse<GetAuthorResponse>> UpdateAuthor(int id, UpdateAuthorRequest request)
    {
        var response = _authorService.UpdateAuthor(id, request);
        return StatusCode(response.StatusCode, response);
    }

    // [AuthorizedRoles(ROLES.LIBRARIAN)]
    [HttpDelete("/{id}")]
    public ActionResult<ApiResponse<GetAuthorResponse>> DeleteAuthor(int id)
    {
        var response = _authorService.DeleteAuthor(id);
        if (response.Success)
        {
            return NoContent();
        }
        return StatusCode(response.StatusCode, response);
    }
}
