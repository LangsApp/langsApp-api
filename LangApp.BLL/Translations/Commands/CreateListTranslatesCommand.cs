using LangApp.BLL.Validation;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Interfaces.Services;
using LangApp.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace LangApp.BLL.Translations.Commands
{
    public record CreateListTranslatesCommand() : IRequest<bool>;

    public class CreateListTranslatesCommandHandler(ITranslateRepository transRepo, ILangCodeRepository langCodeRepo, 
        IBaseWordRepository baseWordRepo, ITranslateService libreTranslateService,
        ILogger<CreateListTranslatesCommand> logger) : IRequestHandler<CreateListTranslatesCommand, bool>
    {
        public async Task<bool> Handle(CreateListTranslatesCommand reqest, CancellationToken cancellationToken)
        {
            var existingTranslates = await transRepo.GetAllTranslatesAsync();
            var existingLangCodes = await langCodeRepo.GetAllLanguagesAsync();
            var existingBaseWords = await baseWordRepo.GetAllBaseWordsAsync();

            var skippedPairs = new Dictionary<string, string>();

            var existingPairs = existingTranslates
            .Select(t => (t.WordId, t.LanguageId))
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


                    var normalizedTranslatedText = TextValidation.IsValidText(translatedText!);

                    if(!normalizedTranslatedText)
                    {
                        skippedPairs.Add(baseWord.NormalizedWord, langCode.Name);
                        continue;
                    }

                    newTranslates.Add(new Translate
                    {//викликати libreTranslate для отримання перекладу
                        WordId = baseWord.Id,
                        LanguageId = langCode.Id,
                        NormalizedTranslatedText = translatedText!.ToLower(),
                        DisplayTranslatedText = translatedText
                    });
                }
            }
            if (newTranslates.Count > 0)
            {
                await transRepo.AddListTranslatesAsync(newTranslates);
               logger.LogInformation("Created {Count} new translates", newTranslates.Count);    
                return true;
            }
            logger.LogInformation("No new translates were created");
            return false;
        }
    }

}
