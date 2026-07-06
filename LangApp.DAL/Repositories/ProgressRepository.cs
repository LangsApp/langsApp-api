using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.DAL.Repositories
{
    public class ProgressRepository(LangAppDBContext dBContext) : IProgressRepository
    {
        public Task<Progress> AddProgressAsync(Progress newProgress)
        {
          throw new NotImplementedException();
        }

        public async Task<List<Progress>> AddListProgressAsync(List<Progress> newProgresses)
        {
            await dBContext.Progress.AddRangeAsync(newProgresses);
            await dBContext.SaveChangesAsync();
            return newProgresses;
        }

        public async Task<List<Progress>> AchieveStageAsync(List<string> userAnswers, string userId, Guid nativeLangId)
        {
            var normalizedUserAnswers = userAnswers.Select(q => q.Trim().ToLower()).ToList();

            var translates = await dBContext.Translate
                .Where(t => t.LanguageId == nativeLangId && normalizedUserAnswers.Contains(t.NormalizedTranslatedText))
                .ToListAsync();

            var userProgress = await dBContext.Progress.Where(p => p.UserId == userId && p.LangCodeId == nativeLangId && translates.Select(t => t.WordId).Contains(p.WordId))
                .ToListAsync();


            if (userProgress.Count == 0)
            {
                throw new Exception("No matching progress found for the provided user answers.");
            }

            var stages = await dBContext.Stage.ToListAsync();

            foreach (var progress in userProgress)
            {
               var currentStage = stages.FirstOrDefault(s => s.Id == progress.StageId)
                    ?? throw new Exception($"Current stage with ID {progress.StageId} not found.");

                var nextStage = stages.Where(s => s.Order > currentStage.Order)
                     .OrderBy(s => s.Order)
                     .FirstOrDefault()
                    ?? throw new Exception($"No next stage found for current stage {currentStage.StageName}.");
               progress.StageId = nextStage.Id;
            }

            await dBContext.SaveChangesAsync();

            return await dBContext.Progress.Where(p => p.UserId == userId && p.LangCodeId == nativeLangId).ToListAsync();
        }

        public async Task<List<Progress>> GetUserProgressAsync(string userId, Guid langId)
        {
            return await dBContext.Progress
                .Where(p => p.UserId == userId && p.LangCodeId == langId)
                .Include(p => p.Stage)
                .ToListAsync();
        }
    }
}
