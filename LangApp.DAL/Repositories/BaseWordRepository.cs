

using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<BaseWord>> AddListWordsAsync(List<BaseWord> newWords)
    {
        
        dbContext.BaseWord.AddRange(newWords);
        await dbContext.SaveChangesAsync();
        return newWords;
    }

    public async Task<BaseWord?> GetBaseWordByNameAsync(string normalizedWord)
    {
        return await dbContext.BaseWord.FirstOrDefaultAsync(w => w.NormalizedWord == normalizedWord);
    }

    public async Task<ICollection<BaseWord>> GetAllBaseWordsAsync()
    {
        return await dbContext.BaseWord.ToListAsync();
    }

    public async Task<HashSet<string>> GetAllNormalizedWordsAsync()
    {
        var normalizedWords = await dbContext.BaseWord.Select(w => w.NormalizedWord).ToListAsync();
        return [.. normalizedWords];
    }
}
