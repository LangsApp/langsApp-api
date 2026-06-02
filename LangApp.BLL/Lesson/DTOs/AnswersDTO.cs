using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Lesson.DTOs
{
    public class AnswersDTO
    {
        public Guid WordId { get; set; }
        public string UserAnswer { get; set; } = string.Empty;
    }
}
