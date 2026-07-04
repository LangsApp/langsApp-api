using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using LangApp.Core.QueryResults;
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
        public async Task<LessonWordsResult> GetLessonWordsAsync(Languages langTo, string userId)
        {
            var minOrder = await dbContext.Progress
                .Where(p =>
                p.UserId == userId &&
                p.LangCodeId == langTo.Id &&
                p.Stage!.Order < 100)
                .MinAsync(p => (int?)p.Stage!.Order);

            var baseWords = await dbContext.BaseWord
                .Where(w => dbContext.Progress
                .Any(p => p.WordId == w.Id && p.UserId == userId && p.LangCodeId == langTo.Id
                && p.Stage!.Order == minOrder))
                .Take(20)
                .ToListAsync();

            var fromProgress = baseWords.Count > 0;

            if (baseWords.Count == 0)
            {
                baseWords = await dbContext.BaseWord
                .Where(w => !dbContext.Progress
                .Any(p => p.WordId == w.Id && p.UserId == userId && p.LangCodeId == langTo.Id))
                .Take(20)
                .ToListAsync();
            }

            var translates = await dbContext.Translate
                .Where(t => baseWords.Select(b => b.Id).Contains(t.WordId))
                .ToListAsync();

            return new LessonWordsResult
            {
                Translates = translates,
                FromProgress = fromProgress
            };
        }
    }
}
