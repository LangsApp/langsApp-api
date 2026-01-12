using LangApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangApp.DAL.Configurations;

internal class ProgressConfig : IEntityTypeConfiguration<Progress>
{
    public void Configure(EntityTypeBuilder<Progress> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(u => u.User)
               .WithMany(p => p.Progresses)
               .HasForeignKey(p => p.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.Word)
               .WithMany()
               .HasForeignKey(p => p.WordId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Stage)
                .WithMany()
                .HasForeignKey(p => p.StageId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => new { p.UserId, p.WordId, p.StageId })
               .IsUnique();

    }
}
