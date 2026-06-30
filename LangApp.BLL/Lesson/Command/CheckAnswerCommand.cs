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
    public record CheckAnswerCommand(string UserId, List<AnswersDTO> Answers, string langTo) : IRequest<CheckAnswerResultDTO>;

    public class CheckAnswerCommandHandler(
        ITranslateRepository translateRepository,
        ILangCodeRepository langCodeRepository,
        IProgressRepository progressRepository) : IRequestHandler<CheckAnswerCommand, CheckAnswerResultDTO>
    {
        public async Task<CheckAnswerResultDTO> Handle(CheckAnswerCommand request, CancellationToken cancellationToken)
        {
            var uncorrectAnswers = new List<AnswersDTO>();

            var correctAnswers = new List<AnswersDTO>();

            var langToId = await langCodeRepository.GetLangCodeByCodeAsync(request.langTo)
                ?? throw new Exception($"Language code '{request.langTo}' not found.");
            
            var correctAnswerWords = await translateRepository
                .GetAnswersByQuestionsAsync(request.Answers.Select(x => x.Question).ToList(), langToId);
            
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
            
            await progressRepository.AchieveStageAsync(correctAnswers.Select(a => a.UserAnswer).ToList(), 
                                                       request.UserId, langToId.Id);

            return new CheckAnswerResultDTO
            {
                CorrectAnswers = correctAnswers,
                UncorrectAnswers = uncorrectAnswers
            };
        }
    }
}
