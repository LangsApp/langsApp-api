using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Interfaces.Repository
{
    public interface IProgressRepository
    {
        Task<Progress> AddProgressAsync(Progress newProgress);
        Task<List<Progress>> AddListProgressAsync (List<Progress> newProgress);
        Task<List<Progress>> AchieveStageAsync(List<string> userAnswers, string userId, Guid langId);

        Task<List<Progress>> GetUserProgressAsync(string userId, Guid langId);
    }
}
