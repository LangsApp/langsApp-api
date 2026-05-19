using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.DAL.Repositories
{
    public class LessonRepository(LangAppDBContext dbContext) : ILessonRepository
    {
        public async Task<ICollection<Translate>> GetLessonWordsAsync(Languages langTo, string userId)
        {
            var baseWords = await dbContext.BaseWord
                .Where(w => !dbContext.Progress
                .Any(p => p.WordId == w.Id && p.UserId == userId && p.LangCodeId == langTo.Id))
                .Take(20)
                .ToListAsync();

            var translates = await dbContext.Translate
                .Where(t => baseWords.Select(b => b.Id).Contains(t.WordId))
                .ToListAsync();

            //var wordsFrom = lessonWords
            //    .Where(t => t.LanguageId == langFrom.Id)
            //    .Select(t => t.DisplayTranslatedText)
            //    .ToList();

            //var wordsTo = lessonWords
            //    .Where(t => t.LanguageId == langTo.Id)
            //    .Select(t => t.DisplayTranslatedText)
            //    .ToList();

            //var result = new 
            //{
            //    wordsFrom,
            //    wordsTo
            //};

            return translates;
        }
    }
}
