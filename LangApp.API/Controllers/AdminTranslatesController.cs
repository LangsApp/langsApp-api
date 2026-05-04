using LangApp.BLL.Translations.Commands;
using LangApp.Core.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers;

[Authorize(Roles = UserRoles.SuperAdmin)]
[Route("api/[controller]")]
[ApiController]
public class AdminTranslatesController(ISender sender, ILogger<AdminTranslatesController> logger) : ControllerBase
{
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
}
