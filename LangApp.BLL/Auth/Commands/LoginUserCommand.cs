using LangApp.BLL.Auth.DTOs;
using LangApp.BLL.Auth.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LangApp.BLL.Auth.Commands
{// IAuthService не використовуємо. Login (формування токену) реалізується одразу ось тут
 // Створюємо окремий інтерфейс з методами по отриманню користувачів, і один із методів
 // буде діставати із БД юзера по Email і тягти його сюди
    public record LoginUserCommand(LoginDTO loginDTO) : IRequest<string>;

    public class LoginUserCommandHandler(IAuthService repo, ILogger<LoginUserCommandHandler> _logger)
        : IRequestHandler<LoginUserCommand, string>
    {
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling LoginUserCommand for user with email: {Email}", request.loginDTO.Email);
            var token = await repo.Login(request.loginDTO);
            return token;
        }
    }
}
