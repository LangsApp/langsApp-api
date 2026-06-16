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
        ILangCodeRepository langCodeRepository) : IRequestHandler<CheckAnswerCommand, CheckAnswerResultDTO>
    {
        public async Task<CheckAnswerResultDTO> Handle(CheckAnswerCommand request, CancellationToken cancellationToken)
        {
            var uncorrectAnswers = new List<AnswersDTO>();

            var correctAnswers = new List<AnswersDTO>();

            var langFromId = await langCodeRepository.GetLangCodeByCodeAsync(request.langTo)
                ?? throw new Exception($"Language code '{request.langTo}' not found.");
            
            var correctAnswerWords = await translateRepository
                .GetAnswersByQuestionsAsync(request.Answers.Select(x => x.Question).ToList(), langFromId);
            
            foreach (var answer in request.Answers)
            {
                if(correctAnswerWords.Any(t =>
                t.Equals(answer.UserAnswer, StringComparison.CurrentCultureIgnoreCase)))
                {
                    correctAnswers.Add(answer);
                }
                else
                {
                    uncorrectAnswers.Add(answer);
                }
            }

            return new CheckAnswerResultDTO
            {
                CorrectAnswers = correctAnswers,
                UncorrectAnswers = uncorrectAnswers
            };
        }
    }
}
