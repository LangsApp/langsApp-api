using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Interfaces.Repository
{
    public interface IStageRepository
    {
        Task<Stage> CreateStageAsync(Stage newStage);
        Task<Stage?> GetStageByNameAsync(string stageName);
        Task<List<Stage>> GetAllStagesAsync();
    }
}
