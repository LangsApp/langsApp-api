using LangApp.BLL.Validation;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Interfaces.Services;
using LangApp.Core.Models;
using LangApp.BLL.Translations.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace LangApp.BLL.Translations.Commands
{
    public record CreateListTranslatesCommand() : IRequest<CreateListTranslatesResponseDTO>;

    public class CreateListTranslatesCommandHandler(
        ITranslateRepository transRepo, 
        ILangCodeRepository langCodeRepo, 
        IBaseWordRepository baseWordRepo, 
        ITranslateService libreTranslateService,
        ILogger<CreateListTranslatesCommand> logger) 
        : IRequestHandler<CreateListTranslatesCommand, CreateListTranslatesResponseDTO>
    {
        public async Task<CreateListTranslatesResponseDTO> Handle(CreateListTranslatesCommand reqest, 
            CancellationToken cancellationToken)
        {
            var existingLangCodes = await langCodeRepo.GetAllLanguagesAsync();
            var existingBaseWords = await baseWordRepo.GetAllBaseWordsAsync();

            if (existingLangCodes.Count == 0 || existingBaseWords.Count == 0)
            {
                logger.LogInformation("No new translates were created because " +
                    "there are no base words or language codes");
                return new CreateListTranslatesResponseDTO
                {
                    Count = 0,
                    Message = "No new translates were created because there are no base words or language codes"
                };
            }


            var skippedPairs = new HashSet<(string Word, string LangCode)>();

            var skippedCodes = new HashSet<string>();

            var existingTranslates = await transRepo.GetAllTranslatesAsync();

            var existingPairs = existingTranslates
            .Select(t => (t.WordId, t.LanguageId))
            .ToHashSet();

            var newTranslates = new List<Translate>();

            var codes = await libreTranslateService.GetSupportedLanguagesAsync(cancellationToken);


            foreach (var baseWord in existingBaseWords)
            {
                foreach (var langCode in existingLangCodes)
                {
                    if (existingPairs.Contains((baseWord.Id, langCode.Id)))
                        continue;

                    if(!codes.Contains(langCode.LangCode))
                    {
                        skippedCodes.Add(langCode.LangCode);
                        continue;
                    }

                    var translatedText = await libreTranslateService.TranslateAsync(
                        baseWord.NormalizedWord,
                        "en",
                        langCode.LangCode,
                        cancellationToken
                        );


                    var validTranslatedText = TextValidation.IsValidText(translatedText!);

                    if (!validTranslatedText)
                    {
                        skippedPairs.Add((baseWord.NormalizedWord, langCode.Name));
                        continue;
                    }

                    newTranslates.Add(new Translate
                    {
                        WordId = baseWord.Id,
                        LanguageId = langCode.Id,
                        NormalizedTranslatedText = TextNormalizer.ToNormalized(translatedText!),
                        DisplayTranslatedText = TextNormalizer.ToDisplay(translatedText!)
                    });
                }
            }


            var message = new List<string>();

            if(skippedPairs.Count > 0)
            {
                message.Add($"Created new translates. Some translations were skipped: {string.Join(", ", 
                    skippedPairs.Select(p => $"({p.Word} - {p.LangCode})"))}");
            }

            if (skippedCodes.Count > 0)
            {
                message.Add($"Created new translates. Some codes were skipped: " +
                    $"{string.Join(", ", skippedCodes)}");
            }

            if (newTranslates.Count > 0)
            {
                await transRepo.AddListTranslatesAsync(newTranslates);
               logger.LogInformation("Created {Count} new translates", newTranslates.Count);    
                return new CreateListTranslatesResponseDTO { 
                    Count = newTranslates.Count,
                    Message = message.Count > 0 ? string.Join("\n", message) : 
                    $"Created {newTranslates.Count} new translates"
                };
            }

            logger.LogInformation("No new translates were created");


            return new CreateListTranslatesResponseDTO
            {
                Count = newTranslates.Count,
                Message = $"No new translates were created"
            };
        }
    }

}
