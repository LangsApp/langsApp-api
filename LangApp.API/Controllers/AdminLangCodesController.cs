using LangApp.BLL.LangCode.DTOs;
using LangApp.Core.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LangApp.BLL.LangCode.Commands;

namespace LangApp.API.Controllers;

[Authorize(Roles = UserRoles.SuperAdmin)]
[Route("api/[controller]")]
[ApiController]
public class AdminLangCodesController(ISender sender, ILogger<AdminLangCodesController> _logger) : ControllerBase
{
    [HttpPost("add-langCode")]
    public async Task<IActionResult> AddLanguageCodeAsync([FromBody] CreateLangCodeDTO newLangCode)
    {
        _logger.LogInformation("Received request to add language code: {LanguageCode}", newLangCode.LangCode);
        var result = await sender.Send(new CreateLanguageCodeCommand(newLangCode));
        if (result is null)
        {
            _logger.LogError("Failed to add language code: {LanguageCode}", newLangCode.LangCode);
            return BadRequest("Failed to add the specified language code.");
        }
        _logger.LogInformation("Successfully added language code: {LanguageCode}", newLangCode.LangCode);
        return Ok(result);
    }
}
