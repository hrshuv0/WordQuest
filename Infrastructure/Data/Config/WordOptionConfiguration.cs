using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class WordOptionConfiguration : IEntityTypeConfiguration<WordOption>
{
    public void Configure(EntityTypeBuilder<WordOption> builder)
    {
        builder.ToTable("WordOption");
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.IsCorrect).IsRequired();

        
    }
}