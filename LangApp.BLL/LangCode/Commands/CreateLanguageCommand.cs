using AutoMapper;
using LangApp.BLL.LangCode.DTOs;
using LangApp.BLL.LangCode.Services;
using LangApp.BLL.Validation;
using LangApp.Core.Models;
using MediatR;
using LangApp.BLL.Exceptions;
using Microsoft.Extensions.Logging;
using LangApp.Core.Interfaces.Repository;

namespace LangApp.BLL.LangCode.Commands;

public record CreateLanguageCodeCommand(CreateLangCodeDTO NewLangCode) : IRequest<Languages>;

public class CreateLanguageCommandHandler(ILangCodeRepository repository, IMapper mapper) 
    : IRequestHandler<CreateLanguageCodeCommand, Languages>
{
    public async Task<Languages> Handle(CreateLanguageCodeCommand request, CancellationToken cancellationToken)
    {
         
        if (!TextValidation.IsValidText(request.NewLangCode.Name) 
            || !TextValidation.IsValidText(request.NewLangCode.LangCode))
        { 
            throw new ArgumentException("Invalid language name or code.");
        }

        var entity = mapper.Map<Languages>(request.NewLangCode);

        var normalizedLangCode = LangCodeService.NormalizeLanguage(entity);

        

        var existingLangCode = await repository.GetLangCodeByCodeAsync(normalizedLangCode.LangCode);
        var existingLangName = await repository.GetLangCodeByNameAsync(normalizedLangCode.Name);
        if (existingLangCode != null || existingLangName != null)
        {
            throw new ConflictException("This language already exists.");
        }
            return await repository.CreateLanguageAsync(entity);
    }
}
