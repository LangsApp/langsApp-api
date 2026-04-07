using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.Queries
{
    public record GetAllBaseWordsQuery() : IRequest<ICollection<BaseWord>>;

    public class GetAllBaseWordsCommandHandler(IBaseWord baseWordRepo)
        : IRequestHandler<GetAllBaseWordsQuery, ICollection<BaseWord>>
    {
        public async Task<ICollection<BaseWord>> Handle(GetAllBaseWordsQuery request, CancellationToken cancellationToken)
        {
            return await baseWordRepo.GetAllBaseWordsAsync();
        }
    }
}