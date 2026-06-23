using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using System;
using System.Collections.Generic;
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
            dBContext.Progress.AddRange(newProgresses);
            await dBContext.SaveChangesAsync();
            return newProgresses;
        }
    }
}
