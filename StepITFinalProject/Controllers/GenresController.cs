using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepITFinalProject.Requests;
using StepITFinalProject.Services.Interfaces;
using StepITFinalProject.Responses;
using StepITFinalProject.Common;
using StepITFinalProject.Enums;
using StepITFinalProject.Attributes;
namespace StepITFinalProject.Controllers;

[Route("api/genres")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;
    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [AuthorizedRoles(ROLES.READER, ROLES.LIBRARIAN)]
    [HttpGet]
    public ActionResult<ApiResponse<List<GetGenreResponse>>> GetAllGenres()
    {
        var response = _genreService.GetAllGenres();
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [AuthorizedRoles(ROLES.LIBRARIAN)] 
    public ActionResult<ApiResponse<CreateGenreResponse>> CreateGenre(CreateGenreRequest request)
    {
        var response = _genreService.CreateGenre(request);
        return StatusCode(response.StatusCode, response);
    }
}
