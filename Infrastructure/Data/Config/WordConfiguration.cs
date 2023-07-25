using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class WordConfiguration : IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
        builder.ToTable("Word");
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.PartOfSpeech).HasMaxLength(100);
        builder.Property(p => p.Pronunciation).HasMaxLength(100);
        builder.Property(p => p.Definition).HasMaxLength(250);
        builder.Property(p => p.Example).HasMaxLength(250);
        builder.Property(p => p.Translation).HasMaxLength(250);
    }
}