using LangApp.BLL.Auth.Commands;
using LangApp.BLL.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers.Accounts
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(ISender sender, ILogger<AccountController> _logger) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await sender.Send(new LoginUserCommand(loginDTO));
            if (result != null)
            {
                _logger.LogInformation("User logged in successfully.");
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Failed login attempt for email: {Email}", loginDTO.Email);
                return Unauthorized();
            }
        }
    }
}
