using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace LangApp.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LangAppDBContext _dbContext;

        public UserRepository(LangAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<User?> GetUserByEmailAsync(string email)
        //{
        //    return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        //}
    }
}
