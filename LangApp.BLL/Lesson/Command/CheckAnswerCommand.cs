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
    public record CheckAnswerCommand(string UserId, List<AnswersDTO> Answers) : IRequest<CheckAnswerResultDTO>;

    public class CheckAnswerCommandHandler(
        ITranslateRepository translateRepository) : IRequestHandler<CheckAnswerCommand, CheckAnswerResultDTO>
    {
        public async Task<CheckAnswerResultDTO> Handle(CheckAnswerCommand request, CancellationToken cancellationToken)
        {
            //var wordIdsAnswers = request.Answers.Select(x => x.WordId).ToList();

            //var BaseWordIds = await translateRepository.GetCorrectIDsAnswersAsync(wordIdsAnswers);

            var uncorrectAnswers = new List<AnswersDTO>();

            var correctAnswers = new List<AnswersDTO>();

            var correctAnswerWords = await translateRepository
                .GetAnswersByQuestionsAsync(request.Answers.Select(x => x.Question).ToList());

            foreach (var answer in request.Answers)
            {
                if(correctAnswerWords.Any(t => t.NormalizedTranslatedText
                .Equals(answer.UserAnswer, StringComparison.CurrentCultureIgnoreCase)))
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
