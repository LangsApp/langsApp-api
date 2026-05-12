using AutoMapper;
using LangApp.BLL.Exceptions;
using LangApp.BLL.Validation;
using LangApp.BLL.Words.DTOs;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Interfaces.Services;
using LangApp.Core.Models;
using MediatR;

namespace LangApp.BLL.Words.Commands;

public record CreateBaseWordCommand(CreateBaseWordDTO NewWord) : IRequest<BaseWord>;

public class CreateBaseWordCommandHandler(IBaseWordRepository repository) 
    : IRequestHandler<CreateBaseWordCommand, BaseWord>
{
    public async Task<BaseWord> Handle(CreateBaseWordCommand request, CancellationToken cancellationToken)
    {   
        if(!TextValidation.IsValidText(request.NewWord.NormalizedWord))
            throw new ArgumentException("Invalid word format.");

        //var entity = mapper.Map<BaseWord>(request.NewWord);
        var entity = new BaseWord
        {
            NormalizedWord = TextNormalizer.ToNormalized(request.NewWord.NormalizedWord),
            DisplayWord = TextNormalizer.ToDisplay(request.NewWord.NormalizedWord)
        };
        //var noralizedWord = WordService.NormalizedWord(entity);

        var existingWord = await repository.GetBaseWordByNameAsync(entity.NormalizedWord);
        if (existingWord != null)
        {
            throw new ConflictException($"Word '{existingWord.NormalizedWord}' already exists.");
        }
        return await repository.CreateBaseWordAsync(entity);
    }
}

