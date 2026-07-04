using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Lesson.DTOs
{
    public class WordsForLessonDTO
    {
        public List<LessonWordDTO> WordsFrom {  get; set; } = [];
        //public List<Guid> WordIds { get; set; } = [];
        public string Message { get; set; } = string.Empty;
    }
}
