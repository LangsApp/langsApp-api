using LangApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LangApp.DAL.Configurations;

public class BaseWordConfig : IEntityTypeConfiguration<BaseWord>
{
    public void Configure(EntityTypeBuilder<BaseWord> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.NormalizedWord)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(b => b.NormalizedWord)
               .IsUnique();

        builder.Property(b => b.DisplayWord)
               .HasMaxLength(100)
               .IsRequired(false);
    }
}
