using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.QueryResults
{
    public class LessonWordsResult
    {
        public ICollection<Translate> Translates { get; set; } = [];
        public bool FromProgress { get; set; }
    }
}
