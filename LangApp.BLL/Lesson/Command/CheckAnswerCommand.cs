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
    public record CheckAnswerCommand(string UserId, List<AnswersDTO> Answers, string langFrom) : IRequest<CheckAnswerResultDTO>;

    public class CheckAnswerCommandHandler(
        ITranslateRepository translateRepository,
        ILangCodeRepository langCodeRepository) : IRequestHandler<CheckAnswerCommand, CheckAnswerResultDTO>
    {
        public async Task<CheckAnswerResultDTO> Handle(CheckAnswerCommand request, CancellationToken cancellationToken)
        {
            var uncorrectAnswers = new List<AnswersDTO>();

            var correctAnswers = new List<AnswersDTO>();

            var langFromId = await langCodeRepository.GetLangCodeByCodeAsync(request.langFrom)
                ?? throw new Exception($"Language code '{request.langFrom}' not found.");
            
            var correctAnswerWords = await translateRepository
                .GetAnswersByQuestionsAsync(request.Answers.Select(x => x.Question).ToList(), langFromId);
            //закінчив CheckAnswerCommand і припускаю, що перевірка відповідей буде відпрацьовувати правильно.
            //Потрібно наступного разу як я сяду за ноут, перевірити як воно працює у свагері
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
