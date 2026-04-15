using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using MediatR;

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