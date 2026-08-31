using LangApp.BLL.LangCode.Commands;
using LangApp.BLL.LangCode.DTOs;
using LangApp.BLL.LangCode.Query;
using LangApp.Core.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AdminLangCodesController(ISender sender, ILogger<AdminLangCodesController> _logger) : ControllerBase
{
    [Authorize(Roles = UserRoles.SuperAdmin)]
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



    [HttpGet("get-languages")]
    public async Task<IActionResult> GetLanguagesAsync()
    {
        _logger.LogInformation("Received request to get available languages.");

        var result = await sender.Send(new GetLanguagesQuery());
        if (result is null)
        {
            _logger.LogError("Failed to get available languages.");
            return BadRequest("Failed to get available languages.");
        }
        _logger.LogInformation("Successfully retrieved available languages.");
        return Ok(result);
    }
}
