using LangApp.BLL.Lesson.DTOs;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Lesson.Command
{
    public record PrepareLessonCommand(string LangFrom, string LangTo, string UserId) : IRequest<WordsForLessonDTO>;

    public class PrepareLessonCommandHandler(
        ILessonRepository lessonRepository,
        ILangCodeRepository langCodeRepository,
        ILogger<PrepareLessonCommand> _logger) : IRequestHandler<PrepareLessonCommand, WordsForLessonDTO>
    {
        public async Task<WordsForLessonDTO> Handle(PrepareLessonCommand request, CancellationToken cancellationToken)
        {
            var langCodeFrom = await langCodeRepository.GetLangCodeByCodeAsync(request.LangFrom);
            var langCodeTo = await langCodeRepository.GetLangCodeByCodeAsync(request.LangTo);

            if (langCodeFrom == null || langCodeTo == null)
            {
                _logger.LogError("Language code {LangFrom} or {LangTo} not found.", request.LangFrom, request.LangTo);
                return new WordsForLessonDTO { Message = 
                    $"Language code {request.LangFrom} or {request.LangTo} not found." };
            }

            var translates = await lessonRepository
                .GetLessonWordsAsync(langCodeTo, request.UserId);

            //var wordsFrom = translates.Where(t => t.LanguageId == langCodeFrom.Id)
            //        .Select(t => t.DisplayTranslatedText)
            //        .ToList();

            //var wordsTo = translates.Where(t => t.LanguageId == langCodeTo.Id)
            //        .Where(t => t.WordId == )
            //        .Select(t => t.DisplayTranslatedText)
            //        .ToList();

            var pairs = translates
            .GroupBy(t => t.WordId)
            .Select(g => new
            {
                WordFrom = g.FirstOrDefault(t => t.LanguageId == langCodeFrom.Id)!.DisplayTranslatedText,

                WordTo = g.FirstOrDefault(t => t.LanguageId == langCodeTo.Id)!.WordId
            })
            .ToList();

            var response = new WordsForLessonDTO
            {
                WordsFrom = pairs.Select(p => p.WordFrom).ToList(),
                WordsTo = pairs.Select(p => p.WordTo).ToList(),

                Message = translates.Count > 0 ? "Words for lesson prepared successfully." :
                                            "No words found for the lesson."
            };

            return response;
        }
    }
}
