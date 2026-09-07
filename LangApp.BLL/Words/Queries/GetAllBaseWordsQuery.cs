using LangApp.BLL.Words.DTOs;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.Queries
{
    public record GetAllBaseWordsQuery() : IRequest<ICollection<GetBaseWordsDTO>>;

    public class GetAllBaseWordsQueryHandler(IBaseWordRepository baseWordRepo)
        : IRequestHandler<GetAllBaseWordsQuery, ICollection<GetBaseWordsDTO>>
    {
        public async Task<ICollection<GetBaseWordsDTO>> Handle(GetAllBaseWordsQuery request, CancellationToken cancellationToken)
        {
            var entity = await baseWordRepo.GetAllBaseWordsAsync();

            var response = entity.Select(w => new GetBaseWordsDTO
            {
                NormalizedWord = w.NormalizedWord,
                DisplayWord = w.DisplayWord
            }).ToList();

            return response;
        }
    }
}