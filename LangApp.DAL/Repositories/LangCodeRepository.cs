using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LangApp.DAL.Repositories
{
    public class LangCodeRepository(LangAppDBContext dbContext, ILogger<LangCodeRepository> _logger) : ILangCodeRepository
    {
        public async Task<Languages> CreateLanguageAsync(Languages newLanguage)
        {
            _logger.LogInformation("Creating new language: {LanguageName} ({LanguageCode})",
                    newLanguage.Name, newLanguage.LangCode);

            newLanguage.Id = Guid.NewGuid();
            dbContext.Languages.Add(newLanguage);
            await dbContext.SaveChangesAsync();
            return newLanguage;
        }

        public async Task<ICollection<Languages>> GetAllLanguagesAsync()
        {
            _logger.LogInformation("Retrieving all languages from the database.");

            return await dbContext.Languages.ToListAsync();
        }

        public async Task<Languages?> GetLangCodeByCodeAsync(string langCode)
        {
            _logger.LogInformation("Retrieving language by code: {LanguageCode}", langCode);

            return await dbContext.Languages.FirstOrDefaultAsync(l => l.LangCode == langCode);
        }

        public async Task<Languages?> GetLangCodeByNameAsync(string langName)
        {
            _logger.LogInformation("Retrieving language by name: {LanguageName}", langName);

            return await dbContext.Languages.FirstOrDefaultAsync(l => l.Name == langName);
        }

        public async Task<Languages> UpdateCategoryAsync(Languages updatedLanguage)
        {
            _logger.LogInformation($"Updating langCode {updatedLanguage.Name}, {updatedLanguage.LangCode}");

            dbContext.Languages.Update(updatedLanguage);
            await dbContext.SaveChangesAsync();
            return updatedLanguage;
        }

        public async Task<Languages?> GetLangCodeByIdAsync(Guid langId)
        {
            _logger.LogInformation("Retrieving language by ID: {LanguageId}", langId);
            return await dbContext.Languages.FirstOrDefaultAsync(l => l.Id == langId);
        }

        public async Task<Guid> DeleteLanguageAsync(Languages language)
        {
            dbContext.Languages.Remove(language);
            await dbContext.SaveChangesAsync();
            return language.Id;
        }
    }
}
