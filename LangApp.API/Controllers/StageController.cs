using LangApp.BLL.LangCode.DTOs;
using LangApp.BLL.StageManagment.Command;
using LangApp.BLL.StageManagment.DTOs;
using LangApp.BLL.StageManagment.Query;
using LangApp.Core.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class StageController(ISender sender, ILogger<AdminLangCodesController> _logger) : ControllerBase
{
    [Authorize(Roles = UserRoles.SuperAdmin)]
    [HttpPost("create-stage")]
    public async Task<IActionResult> CreateStageAsync([FromBody] CreateStageDTO createStageDTO)
    {
        var result = await sender.Send(new CreateStageCommand(createStageDTO));
        if (result is null)
        {
            return BadRequest("Failed to create stage.");
        }
        return Ok(result);
    }

    [HttpGet("get-stages")]
    public async Task<IActionResult> GetStagesAsync()
    {
        _logger.LogInformation("Received request to get available stages.");
        var result = await sender.Send(new GetStagesQuery());

        if (result is null)
        {
            _logger.LogError("Failed to get avaliable stages");
            return BadRequest("Failed to get avaliable stages");
        }
        return Ok(result);
    }
}