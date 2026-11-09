using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.DAL.Repositories;

public class CategoryRepository(LangAppDBContext dbContext) : ICategoryRepository
{
    public async Task<Category> AddCategoryAsync(Category newCategory)
    {
        Console.WriteLine($"Adding new category: {newCategory}");
        newCategory.Id = Guid.NewGuid();
        dbContext.Category.Add(newCategory);
        await dbContext.SaveChangesAsync();
        return newCategory;
    }
    public async Task<Category?> GetCategoryByNameAsync(string categoryName)
    {
        return await dbContext.Category.FirstOrDefaultAsync(c => c.Name == categoryName.ToLower());
    }

    public async Task<ICollection<Category>> GetAllCategoriesAsync()
    {
        return await dbContext.Category.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        //Console.WriteLine($"\nRequest to edit Category id: {id}\n");
        return await dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> UpdateCategoryAsync(Category updatedCategory)
    {
        Console.WriteLine($"\nUpdated category id: {updatedCategory.Id}\n");
        dbContext.Category.Update(updatedCategory);
        await dbContext.SaveChangesAsync();
        return updatedCategory;
    }

    public async Task<Guid> DeleteCategoryAsync(Category category)
    {
        dbContext.Category.Remove(category);
        await dbContext.SaveChangesAsync();
        return category.Id;
    }

    public async Task<ICollection<Category>> GetCategoriesByIdsAsync(IEnumerable<Guid> ids)
    {
        return await dbContext.Category.Where(c => ids.Contains(c.Id)).ToListAsync();
    }

    public async Task<ICollection<Category>> ReviewedOrPublishedCategoriesAsync(IEnumerable<Category> categories)
    {
        dbContext.Category.UpdateRange(categories);
        await dbContext.SaveChangesAsync();
        return categories.ToList();
    }
}