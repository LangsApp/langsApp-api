using LangApp.BLL.Lesson.DTOs;
using LangApp.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Lesson.Command
{
    public record CheckAnswerCommand(string UserId, List<AnswersDTO> Answers, string NativeLang) : IRequest<CheckAnswerResultDTO>;

    public class CheckAnswerCommandHandler(
        ITranslateRepository translateRepository,
        ILangCodeRepository langCodeRepository,
        IProgressRepository progressRepository) : IRequestHandler<CheckAnswerCommand, CheckAnswerResultDTO>
    {
        public async Task<CheckAnswerResultDTO> Handle(CheckAnswerCommand request, CancellationToken cancellationToken)
        {
            var uncorrectAnswers = new List<AnswersDTO>();

            var correctAnswers = new List<AnswersDTO>();

            var nativeLangId = await langCodeRepository.GetLangCodeByCodeAsync(request.NativeLang)
                ?? throw new Exception($"Language code '{request.NativeLang}' not found.");

            var correctAnswerWords = await translateRepository
                .GetAnswersByQuestionsAsync(request.Answers.Select(x => x.Question).ToList(), nativeLangId);
            
            foreach (var answer in request.Answers)
            {
                if(correctAnswerWords.Any(t =>
                t.NormalizedTranslatedText.Equals(answer.UserAnswer, StringComparison.CurrentCultureIgnoreCase)))
                {
                    correctAnswers.Add(answer);
                }
                else
                {
                    uncorrectAnswers.Add(answer);
                }
            }
            
            if(correctAnswers.Count > 0)
            {
                await progressRepository.AchieveStageAsync(correctAnswers.Select(a => a.UserAnswer).ToList(),
                                                      request.UserId, nativeLangId.Id);
            }
               

            return new CheckAnswerResultDTO
            {
                CorrectAnswers = correctAnswers,
                UncorrectAnswers = uncorrectAnswers
            };
        }
    }
}
