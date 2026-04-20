using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LangApp.BLL.Translations.Commands
{
    public record CreateListTranslatesCommand() : IRequest<Translate>;

    public class CreateListTranslatesCommandHandler(ITranslate transRepo, ILangCode langCodeRepo, IBaseWord baseWordRepo)
    {
        public async Task<bool> Handle(CancellationToken cancellationToken)
        {
            var existingTranslates = await transRepo.GetAllTranslatesAsync();
            var existingLangCodes = await langCodeRepo.GetAllLanguagesAsync();
            var existingBaseWords = await baseWordRepo.GetAllBaseWordsAsync();

            var existingPairs = existingTranslates
            .Select(t => (t.WordId, t.LangCodeId))
            .ToHashSet();

            var newTranslates = new List<Translate>();

            foreach (var baseWord in existingBaseWords)
            {
                foreach (var langCode in existingLangCodes)
                {
                    if (existingPairs.Contains((baseWord.Id, langCode.Id)))
                        continue;

                    newTranslates.Add(new Translate
                    {//викликати libreTranslate для отримання перекладу
                        WordId = baseWord.Id,
                        LangCodeId = langCode.Id,
                        NormalizedTranslatedText = "",
                        DisplayTranslatedText = ""
                    });
                }
            }
            if (newTranslates.Count > 0)
            {
                //await transRepo.AddRangeAsync(newTranslates);
            }
            return true;
        }
    }

}
