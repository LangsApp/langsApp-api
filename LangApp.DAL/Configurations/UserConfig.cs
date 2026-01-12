using LangApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangApp.DAL.Configurations;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.Login, x.Email })
               .IsUnique();

        builder.HasIndex(u => u.PhoneNumber)
               .IsUnique();

        builder.Property(u => u.Name)
               .HasMaxLength(100)
               .IsRequired(false);

        builder.Property(u => u.Email)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(u => u.Login)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(u => u.PhoneNumber)
               .HasMaxLength(30)
               .IsRequired(false);

        builder.Property(u => u.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("NOW()");

        builder.Property(u => u.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql("NOW()");
    }
}
