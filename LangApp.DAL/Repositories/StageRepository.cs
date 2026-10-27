using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.DAL.Repositories
{
    public class StageRepository(LangAppDBContext dBContext) : IStageRepository
    {
        public async Task<Stage> CreateStageAsync(Stage newStage)
        {
            dBContext.Stage.Add(newStage);
            await dBContext.SaveChangesAsync();
            return newStage;
        }

        public async Task<List<Stage>> GetAllStagesAsync()
        {
            return await dBContext.Stage.ToListAsync();
        }

        public Task<Stage?> GetStageByNameAsync(string stageName)
        {
            return dBContext.Stage.FirstOrDefaultAsync(s => s.StageName == stageName);
        }

        public async Task<Stage> UpdateStageAsync(Stage updatedStage)
        {
            dBContext.Stage.Update(updatedStage);
            await dBContext.SaveChangesAsync();
            return updatedStage;
        }

        public async Task<Stage?> GetStageByIdAsync(Guid stageId)
        {
            return await dBContext.Stage.FirstOrDefaultAsync(s => s.Id == stageId);
        }

        public async Task<Guid> DeleteStageAsync(Stage stage)
        {
            dBContext.Stage.Remove(stage);
            await dBContext.SaveChangesAsync();
            return stage.Id;
        }
    }
}
