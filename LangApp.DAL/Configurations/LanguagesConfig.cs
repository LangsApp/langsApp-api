using LangApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangApp.DAL.Configurations;

public class LanguagesConfig : IEntityTypeConfiguration<Languages>
{
    public void Configure (EntityTypeBuilder<Languages> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(l => l.LangCode)
               .IsRequired()
               .HasMaxLength(5);

        builder.HasIndex(l => l.LangCode)
               .IsUnique();
    }
}
