using LangApp.BLL.Translations.Commands;
using LangApp.BLL.Translations.Queries;
using LangApp.Core.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TranslatesController(ISender sender, ILogger<TranslatesController> logger) : ControllerBase
{
    [Authorize(Roles = UserRoles.SuperAdmin)]
    [HttpPost("create-list-translates")]
    public async Task<IActionResult> CreateListTranslatesAsync()
    {
        var result = await sender.Send(new CreateListTranslatesCommand());
        if (result is null)
        {
            logger.LogError("Failed to create list of translates.");
            return BadRequest("Failed to create list of translates.");
        }
        logger.LogInformation(result.Message);
        return Ok(result);
    }

    [HttpGet("get-all-translations")]
    public async Task<IActionResult> GetTranslationsAsync()
    {
        logger.LogInformation("Received request to get available translates.");
        var result = await sender.Send(new GetTranslationsQuery());

        if(result is null)
        {
            logger.LogError("Failed to get available translates.");
            return BadRequest("Failed to get available translates.");
        }

        logger.LogInformation("Successfully retrieved available translates.");
        return Ok(result);
    }
}
