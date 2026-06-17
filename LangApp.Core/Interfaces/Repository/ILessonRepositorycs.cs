using LangApp.Core.Models;
using LangApp.Core.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Interfaces.Repository
{
    public interface ILessonRepository
    {
        Task<LessonWordsResult> GetLessonWordsAsync(Languages langTo, string userId);
    }
}
