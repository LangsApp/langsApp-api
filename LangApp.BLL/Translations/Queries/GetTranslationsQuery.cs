using LangApp.BLL.Translations.DTOs;
using LangApp.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Translations.Queries
{
    public record GetTranslationsQuery() : IRequest<ICollection<GetAllTranslationsDTO>>;
    public class GetTranslationsQueryHandler(ITranslateRepository translateRepository)
        : IRequestHandler<GetTranslationsQuery, ICollection<GetAllTranslationsDTO>>
    {
        public async Task<ICollection<GetAllTranslationsDTO>> Handle(GetTranslationsQuery request, 
            CancellationToken cancellationToken)
        {
            var translates = await translateRepository.GetAllTranslatesAsync();

            var response = translates.Select(t => new GetAllTranslationsDTO
            {
                BaseWord = t.Word?.DisplayWord ?? string.Empty,
                TranslateTo = t.Language?.Name ?? string.Empty,
                NormalizedTranslatedWord = t.NormalizedTranslatedText ?? string.Empty,
                DisplayTranslatedWord = t.DisplayTranslatedText ?? string.Empty,
            }).ToList();

            return response;
        }
    }
}
