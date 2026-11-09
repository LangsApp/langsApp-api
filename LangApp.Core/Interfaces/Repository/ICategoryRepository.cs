using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Interfaces.Repository;

public interface ICategoryRepository
{
    Task<Category> AddCategoryAsync(Category category);
    Task<Category?> GetCategoryByNameAsync(string categoryName);
    Task<Category?> GetCategoryByIdAsync(Guid id);

    Task<ICollection<Category>> GetAllCategoriesAsync();

    Task<Category> UpdateCategoryAsync(Category category);
    Task<Guid> DeleteCategoryAsync(Category category);
    Task<ICollection<Category>> GetCategoriesByIdsAsync(IEnumerable<Guid> ids);
    Task<ICollection<Category>> ReviewedOrPublishedCategoriesAsync(IEnumerable<Category> categories);
}
