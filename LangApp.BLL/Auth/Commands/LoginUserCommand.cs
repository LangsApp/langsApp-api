using LangApp.BLL.Auth.DTOs;
using LangApp.BLL.Auth.Interfaces;
using MediatR;

namespace LangApp.BLL.Auth.Commands
{// IAuthService не використовуємо. Login (формування токену) реалізується одразу ось тут
 // Створюємо окремий інтерфейс з методами по отриманню користувачів, і один із методів
 // буде діставати із БД юзера по Email і тягти його сюди
    public record LoginUserCommand(LoginDTO loginDTO) : IRequest<string>;

    public class LoginUserCommandHandler(IAuthService repo)
        : IRequestHandler<LoginUserCommand, string>
    {
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var token = await repo.Login(request.loginDTO);
            return token;
        }
    }
}
