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
    public record PrepareLessonCommand(string lerningLang, string NativeLang, string UserId) : IRequest<WordsForLessonDTO>;

    public class PrepareLessonCommandHandler(
        ILessonRepository lessonRepository,
        ILangCodeRepository langCodeRepository,
        IStageRepository stageRepository,
        IProgressRepository progressRepository,
        ILogger<PrepareLessonCommand> _logger) : IRequestHandler<PrepareLessonCommand, WordsForLessonDTO>
    {
        public async Task<WordsForLessonDTO> Handle(PrepareLessonCommand request, CancellationToken cancellationToken)
        {
            var lerningLang = await langCodeRepository.GetLangCodeByCodeAsync(request.lerningLang);
            var nativeLang = await langCodeRepository.GetLangCodeByCodeAsync(request.NativeLang);

            if (lerningLang == null || nativeLang == null)
            {
                _logger.LogError("Language code {LangFrom} or {LangTo} not found.", request.lerningLang, request.NativeLang);
                return new WordsForLessonDTO { Message =
                    $"Language code {request.lerningLang} or {request.NativeLang} not found." };
            }

            var translates = await lessonRepository
                .GetLessonWordsAsync(nativeLang, request.UserId);


            if (!translates.FromProgress)
            {
                var stages = await stageRepository.GetAllStagesAsync();

                var initialStage = stages.OrderBy(s => s.Order).FirstOrDefault();

                if (initialStage == null)
                {
                    _logger.LogError("No stages found in the database.");
                    return new WordsForLessonDTO { Message = "No stages found in the database." };
                }

                var progresses = translates.Translates
                .GroupBy(t => t.WordId)
                .Select(g => new Progress
                {
                    UserId = request.UserId,
                    WordId = g.Key,
                    LangCodeId = nativeLang.Id,
                    StageId = initialStage.Id
                })
                .ToList();

                await progressRepository.AddListProgressAsync(progresses);
            }


            var lessonWordDTO = new List<LessonWordDTO>();
            var userProgress = await progressRepository.GetUserProgressAsync(request.UserId, nativeLang.Id);

            foreach (var translate in translates.Translates.Where(t => t.LanguageId == lerningLang.Id))
            {
                var wordFrom = translate.DisplayTranslatedText;
                var stageName = userProgress.Where(p => p.WordId == translate.WordId && p.LangCodeId == nativeLang.Id)
                    .Select(p => p.Stage?.StageName).FirstOrDefault() ?? "Unknown Stage";

                lessonWordDTO.Add(new LessonWordDTO
                {
                    WordFrom = wordFrom,
                    StageName = stageName
                });
            }

            var response = new WordsForLessonDTO
            {
                WordsFrom = lessonWordDTO.ToList(),
                //WordsFrom = translates.Translates.GroupBy(t => t.WordId).Select(g =>
                //                      g.FirstOrDefault(t => t.LanguageId == langCodeFrom.Id)?
                //                      .DisplayTranslatedText ?? string.Empty).ToList(),


                Message = translates.Translates.Count > 0 ? "Words for lesson prepared successfully." :
                                            "No words found for the lesson."
            };

            return response;
        }
    }
}
