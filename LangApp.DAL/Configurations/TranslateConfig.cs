using LangApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangApp.DAL.Configurations;

internal class TranslateConfig : IEntityTypeConfiguration<Translate>
{
    public void Configure(EntityTypeBuilder<Translate> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(t => new { t.WordId, t.LanguageId })
               .IsUnique();

        builder.Property(x => x.NormalizedTranslatedText)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.DisplayTranslatedText)
               .HasMaxLength(100)
               .IsRequired(false);

        builder.HasOne(w => w.Word)
               .WithMany(t => t.Translates)
               .HasForeignKey(t => t.WordId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(l => l.Language)
               .WithMany(t => t.Translates)
               .HasForeignKey(t => t.LanguageId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
