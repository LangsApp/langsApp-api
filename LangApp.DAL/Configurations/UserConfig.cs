using LangApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangApp.DAL.Configurations;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName)
               .HasMaxLength(100)
               .IsRequired(false);

        builder.Property(u => u.LastName)
               .HasMaxLength(100)
               .IsRequired(false);

        builder.Property(u => u.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("NOW()");

        builder.Property(u => u.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql("NOW()");
    }
}
