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
    }
}
