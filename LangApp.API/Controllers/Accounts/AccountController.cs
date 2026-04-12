using LangApp.BLL.Auth.Commands;
using LangApp.BLL.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers.Accounts;


[Route("api/[controller]")]
[ApiController]
public class AccountController(ISender sender, ILogger<AccountController> _logger) : ControllerBase
{
    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        var result = await sender.Send(new RegisterUserCommand(registerDTO));
        if (result.Succeeded)
        {
            _logger.LogInformation("User registered successfully with email: {Email}", registerDTO.Email);
            return Ok(result);
        }
        else
        {
            _logger.LogWarning("Failed registration attempt for email: {Email}. Errors: {Errors}", 
                registerDTO.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
            return BadRequest(result.Errors);
        }
    }
}