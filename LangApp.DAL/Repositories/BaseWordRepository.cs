

using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;

namespace LangApp.DAL.Repositories;

public class BaseWordRepository(LangAppDBContext dbContext) : IBaseWord
{

    public async Task<BaseWord> CreateBaseWordAsync(BaseWord newWord)
    {
        newWord.Id = Guid.NewGuid();
        dbContext.BaseWord.Add(newWord);
        await dbContext.SaveChangesAsync();
        return newWord;
    }
}
