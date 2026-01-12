using LangApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangApp.DAL.Configurations;

internal class TranslateConfig : IEntityTypeConfiguration<Translate>
{
    public void Configure(EntityTypeBuilder<Translate> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(t => new { t.WordId, t.LangCodeId })
               .IsUnique();

        builder.Property(x => x.NormalizedTranslatedText)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.DisplayTranslatedText)
               .HasMaxLength(100)
               .IsRequired(false);

        builder.HasOne(t => t.Word)
               .WithMany()
               .HasForeignKey(t => t.WordId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Language)
               .WithMany()
               .HasForeignKey(t => t.LangCodeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
