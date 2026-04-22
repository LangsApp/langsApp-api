using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LangApp.BLL.Translations.Commands
{
    public record CreateListTranslatesCommand() : IRequest<bool>;

    public class CreateListTranslatesCommandHandler(ITranslate transRepo, ILangCode langCodeRepo, 
        IBaseWord baseWordRepo, ILibreTranslateService libreTranslateService) : IRequestHandler<CreateListTranslatesCommand, bool>
    {
        public async Task<bool> Handle(CreateListTranslatesCommand reqest, CancellationToken cancellationToken)
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

                    var translatedText = await libreTranslateService.TranslateAsync(
                        baseWord.NormalizedWord,
                        "en",
                        langCode.LangCode,
                        cancellationToken
                        );

                    newTranslates.Add(new Translate
                    {//викликати libreTranslate для отримання перекладу
                        WordId = baseWord.Id,
                        LangCodeId = langCode.Id,
                        NormalizedTranslatedText = translatedText!.ToLower(),
                        DisplayTranslatedText = translatedText
                    });
                }
            }
            if (newTranslates.Count > 0)
            {
                await transRepo.AddListTranslatesAsync(newTranslates);
            }
            return true;
        }
    }

}
