using Microsoft.AspNetCore.Mvc;
using StepITFinalProject.Attributes;
using StepITFinalProject.Common;
using StepITFinalProject.Enums;
using StepITFinalProject.Responses;
using StepITFinalProject.Services.Interfaces;
namespace StepITFinalProject.Controllers;

[Route("api/library-cards")]
[ApiController]
public class LibraryCardsController : ControllerBase
{
    private readonly ILibraryCardService _cardService;
    public LibraryCardsController(ILibraryCardService cardService)
    {
        _cardService = cardService;
    }

    [HttpPost]
    [AuthorizedRoles(ROLES.READER)]
    public ActionResult<ApiResponse<GetLibraryCardResponse>> CreateMyCard()
    {
        var response = _cardService.CreateMyCard(User.GetUserId());
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("me")]
    [AuthorizedRoles(ROLES.READER)]
    public ActionResult<ApiResponse<GetLibraryCardResponse>> GetMyCard()
    {
        var response = _cardService.GetMyCard(User.GetUserId());
        return StatusCode(response.StatusCode, response);
    }
}
