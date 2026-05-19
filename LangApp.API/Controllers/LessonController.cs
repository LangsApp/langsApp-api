using LangApp.BLL.Lesson.Command;
using System.Security.Claims;
using LangApp.BLL.Lesson.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonController(ISender sender, ILogger<LessonController> _logger) : ControllerBase
{
    [HttpPost("start")]
    public async Task<IActionResult> PrepareLessonAsync([FromBody] PrepareLessonCommand command)
    {
        _logger.LogInformation("Received request to start a lesson.");

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var commandWithUser = command with 
        { UserId = userId ?? throw new UnauthorizedAccessException() }; // <- тимчасове
                                                                    //рішення. Бо потрібно буде давати можливість
                                                                    //гостям заходити

        var result = await sender.Send(commandWithUser);
        if (result is null)
        {
            _logger.LogError("Failed to start the lesson.");
            return BadRequest("Failed to start the lesson.");
        }
        _logger.LogInformation("Successfully started the lesson.");
        return Ok(result);
    }
}