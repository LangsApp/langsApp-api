using LangApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LangApp.DAL.Configurations;

internal class StageConfig : IEntityTypeConfiguration<Stage>
{
    public void Configure(EntityTypeBuilder<Stage> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.StageName)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(s => s.Order)
               .IsUnique();

        builder.HasIndex(s => s.StageName)
               .IsUnique();
    }
}
