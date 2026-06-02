using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Lesson.DTOs
{
    public class CheckAnswerResultDTO
    {    
        public List<AnswersDTO> CorrectAnswers { get; set; } = [];
        public List<AnswersDTO> UncorrectAnswers { get; set; } = [];
    }
}
