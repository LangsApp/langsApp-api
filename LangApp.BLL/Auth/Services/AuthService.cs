using LangApp.BLL.Auth.DTOs;
using LangApp.BLL.Auth.Interfaces;
using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LangApp.BLL.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<string> Login(LoginDTO loginDTO)
        {
            //var user = await _userRepository.GetUserByEmailAsync(loginDTO.Email);
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                _logger.LogInformation("User {Email} logged in successfully.", loginDTO.Email);
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim("userId", user.Id)
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:ExpiryMinutes"]!)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)),
                        SecurityAlgorithms.HmacSha256));
                _logger.LogInformation("JWT token generated for user {Email}.", loginDTO.Email);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                _logger.LogWarning("Failed login attempt for user {Email}.", loginDTO.Email);
                throw new UnauthorizedAccessException("Invalid Credentials");
            }

        }
    }
}
