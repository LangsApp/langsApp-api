using AutoMapper;
using LangApp.BLL.Exceptions;
using LangApp.BLL.Words.DTOs;
using LangApp.BLL.Words.Service;
using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LangApp.BLL.Words.Commands;

public record CreateBaseWordCommand(CreateBaseWordDTO NewWord) : IRequest<BaseWord>;

public class CreateBaseWordCommandHandler(IBaseWord repository, IMapper mapper) 
    : IRequestHandler<CreateBaseWordCommand, BaseWord>
{
    public async Task<BaseWord> Handle(CreateBaseWordCommand request, CancellationToken cancellationToken)
    {   
        var entity = mapper.Map<BaseWord>(request.NewWord);
        var noralizedWord = WordService.NormalizedWord(entity);
        var existingWord = await repository.GetBaseWordByNameAsync(noralizedWord.NormalizedWord);
        if (existingWord != null)
        {
            throw new ConflictException($"Word '{existingWord.NormalizedWord}' already exists.");
        }
        return await repository.CreateBaseWordAsync(noralizedWord);
    }
}

