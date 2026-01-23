using LangApp.BLL.Words.DTOs;
using LangApp.Core.Models;
using LangApp.Core.Interfaces;
using MediatR;
using AutoMapper;

namespace LangApp.BLL.Words.Commands;

public record CreateBaseWordCommand(CreateBaseWordDTO NewWord) : IRequest<BaseWord>;





public class CreateBaseWordCommandHandler(IBaseWord repository, IMapper mapper) 
    : IRequestHandler<CreateBaseWordCommand, BaseWord>
{
    public async Task<BaseWord> Handle(CreateBaseWordCommand request, CancellationToken cancellationToken)
    {   
        var entity = mapper.Map<BaseWord>(request.NewWord);
        return await repository.CreateBaseWordAsync(entity);
    }
}

