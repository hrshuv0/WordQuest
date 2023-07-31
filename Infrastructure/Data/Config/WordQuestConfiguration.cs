using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class WordQuestConfiguration : IEntityTypeConfiguration<WordQuest>
{
    public void Configure(EntityTypeBuilder<WordQuest> builder)
    {
        builder.ToTable("WordQuest");
        builder.HasKey(p => new {p.WordId, p.WordOptionId});
        
        builder.HasOne(p => p.Word)
            .WithMany(p => p.WordQuestList)
            .HasForeignKey(p => p.WordId);
        
        builder.HasOne(p => p.WordOption)
            .WithMany(p => p.WordList)
            .HasForeignKey(p => p.WordOptionId);
    }
}