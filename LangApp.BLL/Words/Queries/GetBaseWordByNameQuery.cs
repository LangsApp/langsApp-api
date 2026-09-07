using LangApp.BLL.Words.DTOs;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using MediatR;

namespace LangApp.BLL.Words.Queries
{
    public record GetBaseWordByNameQuery(string Word) : IRequest<GetBaseWordsDTO>;

    public class GetBaseWordQueryHandler(IBaseWordRepository baseWordRepo)
        : IRequestHandler<GetBaseWordByNameQuery, GetBaseWordsDTO?>
    {
        public async Task<GetBaseWordsDTO?> Handle(GetBaseWordByNameQuery request, CancellationToken cancellationToken)
        {
            var entity = await baseWordRepo.GetBaseWordByNameAsync(request.Word);

            var response = new GetBaseWordsDTO
            {
                NormalizedWord = entity!.NormalizedWord,
                DisplayWord = entity.DisplayWord
            };

            return response;
        }
    }
}