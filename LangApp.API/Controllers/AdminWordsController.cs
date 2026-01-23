using Microsoft.AspNetCore.Mvc;
using MediatR;
using LangApp.BLL.Words.DTOs;
using LangApp.BLL.Words.Commands;


namespace LangApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminWordsController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddWordAsync([FromBody] CreateBaseWordDTO newWord)
        {
            var result = await sender.Send(new CreateBaseWordCommand(newWord));
            return Ok(result);
        }
    }
}
