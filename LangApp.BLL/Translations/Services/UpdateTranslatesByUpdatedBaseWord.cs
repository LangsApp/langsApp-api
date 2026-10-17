using LangApp.BLL.Validation;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Interfaces.Services;
using LangApp.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LangApp.BLL.Translations.Services
{
    public class UpdateTranslatesByUpdatedBaseWord(
        ITranslateRepository translateRepository, 
        ITranslateService translateService)
    {
        public async Task UpdateForBaseWordAsync(Guid id, string updatedWord, CancellationToken cancellationToken)
        {
            var translates = await translateRepository.GetTranslateByBaseWordIdAsync(id);
            var updatedTranslations = new List<Translate>();

            foreach (var translate in translates)
            {
                var languageCode = translate.Language?.LangCode;

                if(string.IsNullOrWhiteSpace(languageCode))
                {
                    continue;
                }

                var translatedText = await translateService.TranslateAsync(
                    updatedWord,
                    "en",
                    languageCode,
                    cancellationToken);

                if (string.IsNullOrWhiteSpace(translatedText) ||
                !TextValidation.IsValidText(translatedText))
                {
                    continue;
                }

                translate.NormalizedTranslatedText = TextNormalizer.ToNormalized(translatedText);
                translate.DisplayTranslatedText = TextNormalizer.ToDisplay(translatedText);

                updatedTranslations.Add(translate);
            }

            if(updatedTranslations.Count > 0)
            {
                await translateRepository.UpdateListTranslatesAsync(updatedTranslations);
            }
        }
    }
}
