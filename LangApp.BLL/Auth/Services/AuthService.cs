using LangApp.BLL.Auth.DTOs;
using LangApp.BLL.Auth.Interfaces;
using LangApp.Core.Auth;
using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LangApp.BLL.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, 
            ILogger<AuthService> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return (address.Address == email);
            }
            catch
            {
                return false;
            }
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

        public async Task<IdentityResult> Register(RegisterDTO registerDTO)
        {
           if (!IsValidEmail(registerDTO.Email))
            {
                _logger.LogWarning("Invalid email format attempted for registration: {Email}.", registerDTO.Email);
                return IdentityResult.Failed(
                    new IdentityError 
                    {
                        Code = "InvalidEmailFormat",
                        Description = "Invalid email format." 
                    });
            }

            var userExist = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (userExist != null)
            { 
                _logger.LogWarning("Attempt to register with already existing email: {Email}.", registerDTO.Email);
                return IdentityResult.Failed(
                    new IdentityError 
                    { 
                        Code = "EmailAlreadyExists", 
                        Description = "Email already exists." 
                    });
            }

            if(!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
               _logger.LogInformation("Failed to register user {Email} because role {Role} does not exist.", 
                   registerDTO.Email, UserRoles.User);
                return IdentityResult.Failed(
                    new IdentityError 
                    { 
                        Code = "RoleNotFound", 
                        Description = $"Role '{UserRoles.User}' not found." 
                    });
            }

            var user = new User()
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
            };

            var accountResult = await _userManager.CreateAsync(user, registerDTO.Password);
            if(accountResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
                return IdentityResult.Success;
            }
            else
            {
                _logger.LogWarning("Failed to create user account for {Email}. Errors: {Errors}",
                    registerDTO.Email, string.Join(", ", accountResult.Errors.Select(e => e.Description)));
                return accountResult;
            }
        }
    }
}
