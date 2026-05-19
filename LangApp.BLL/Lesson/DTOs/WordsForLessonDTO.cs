using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Lesson.DTOs
{
    public class WordsForLessonDTO
    {
        public List<string> WordsFrom {  get; set; } = [];
        public List<Guid> WordsTo { get; set; } = [];
        public string Message { get; set; } = string.Empty;
    }
}
