using Microsoft.EntityFrameworkCore;
using LangApp.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LangApp.DAL.DataContext;

public class LangAppDBContext : IdentityDbContext<User>
{
    public LangAppDBContext(DbContextOptions<LangAppDBContext> options) : base(options)
    {
    }

    public DbSet<BaseWord> BaseWord { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Languages> Languages { get; set; }
    public DbSet<Progress> Progress { get; set; }
    public DbSet<Stage> Stage { get; set; }
    public DbSet<Translate> Translate { get; set; }
    //public DbSet<User> User { get; set; }


    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LangAppDBContext).Assembly);
    }
}
