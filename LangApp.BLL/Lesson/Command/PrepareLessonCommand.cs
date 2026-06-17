using LangApp.BLL.Lesson.DTOs;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using LangApp.DAL.Repositories;
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
        IStageRepository stageRepository,
        IProgressRepository progressRepository,
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

            var stages = await stageRepository.GetAllStagesAsync();

            var initialStage = stages.OrderBy(s => s.Order).FirstOrDefault();
            
            if (initialStage == null)
            {
                _logger.LogError("No stages found in the database.");
                return new WordsForLessonDTO { Message = "No stages found in the database." };
            }

            if (!translates.FromProgress)
            {
                var progresses = translates.Translates
                .GroupBy(t => t.WordId)
                .Select(g => new Progress
                {
                    UserId = request.UserId,
                    WordId = g.Key,
                    LangCodeId = langCodeTo.Id,
                    StageId = initialStage.Id
                })
                .ToList();

                await progressRepository.AddListProgressAsync(progresses);
            }

            var response = new WordsForLessonDTO
            {
                WordsFrom = translates.Translates.GroupBy(t => t.WordId).Select(g =>
                                      g.FirstOrDefault(t => t.LanguageId == langCodeFrom.Id)?
                                      .DisplayTranslatedText ?? string.Empty).ToList(),


                Message = translates.Translates.Count > 0 ? "Words for lesson prepared successfully." :
                                            "No words found for the lesson."
            };

            return response;
        }
    }
}
