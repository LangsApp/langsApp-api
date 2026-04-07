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
    public record GetBaseWordByNameQuery(string Word) : IRequest<BaseWord>;

    public class GetBaseWordQueryHandler(IBaseWord baseWordRepo)
        : IRequestHandler<GetBaseWordByNameQuery, BaseWord?>
    {
        public async Task<BaseWord?> Handle(GetBaseWordByNameQuery request, CancellationToken cancellationToken)
        {
            return await baseWordRepo.GetBaseWordByNameAsync(request.Word);
        }
    }
}