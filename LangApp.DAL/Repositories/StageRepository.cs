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
            throw new NotImplementedException();
        }

        public Task<Stage?> GetStageByNameAsync(string stageName)
        {
            return dBContext.Stage.FirstOrDefaultAsync(s => s.StageName == stageName);
        }
    }
}
