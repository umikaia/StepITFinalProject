using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepITFinalProject.Models;

namespace StepITFinalProject.Data.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> b)
    {
        b.HasKey(g => g.Id);
        b.Property(g => g.Name).HasMaxLength(60).IsRequired();
        b.HasIndex(g => g.Name).IsUnique();
    }
}

