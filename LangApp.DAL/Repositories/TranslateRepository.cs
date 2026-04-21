using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.DAL.Repositories;

public class TranslateRepository(LangAppDBContext dBContext) : ITranslate
{
    public async Task<ICollection<Translate>> GetAllTranslatesAsync()
    {
        return await dBContext.Translate.ToListAsync(); 
    }
}
