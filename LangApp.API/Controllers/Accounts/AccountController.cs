using LangApp.BLL.Auth.Commands;
using LangApp.BLL.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers.Accounts
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(ISender sender) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await sender.Send(new LoginUserCommand(loginDTO));
            return Ok(result);
        }
    }
}
