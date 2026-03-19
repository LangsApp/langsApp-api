using LangApp.BLL.Auth.DTOs;
using LangApp.BLL.Auth.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Auth.Commands;

public record RegisterUserCommand(RegisterDTO registerDTO) : IRequest<IdentityResult>;
public class RegisterUserCommandHandler(IAuthService repo, ILogger<RegisterUserCommandHandler> _logger)
    : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RegisterUserCommand for user with email: {Email}", request.registerDTO.Email);
        var result = await repo.Register(request.registerDTO);
        return result;
    }

}
