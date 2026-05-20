using LangApp.BLL.StageManagment.Command;
using LangApp.Core.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers.Admin;

[Authorize(Roles = UserRoles.SuperAdmin)]
[Route("api/[controller]")]
[ApiController]
public class AdminStageController(ISender sender) : ControllerBase
{
    [HttpPost("create-stage")]
    public async Task<IActionResult> CreateStageAsync([FromBody] CreateStageCommand command)
    {
        var result = await sender.Send(command);
        if (result is null)
        {
            return BadRequest("Failed to create stage.");
        }
        return Ok(result);
    }
}