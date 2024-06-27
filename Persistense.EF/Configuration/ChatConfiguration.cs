using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configuration;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder
            .HasKey(ex => ex.Id);

        builder
            .Property(ex => ex.Name)
            .IsRequired()
            .ValueGeneratedNever();

        builder
            .HasMany(ex => ex.Members)
            .WithOne(ex => ex.Chat)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(ex => ex.Messages)
            .WithOne(ex => ex.Chat)
            .OnDelete(DeleteBehavior.Cascade);

    }
}