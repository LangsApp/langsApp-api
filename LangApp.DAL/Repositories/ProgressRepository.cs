using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.DAL.Repositories
{
    public class ProgressRepository : IProgressRepository
    {
        public Task<Progress> AddProgressAsync(Progress newProgress)
        {
          throw new NotImplementedException();
        }
    }
}
