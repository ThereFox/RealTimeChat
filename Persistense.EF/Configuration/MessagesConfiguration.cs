using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configuration;

public class MessagesConfiguration : IEntityTypeConfiguration<SendedMessage>
{
    public void Configure(EntityTypeBuilder<SendedMessage> builder)
    {
        builder
            .HasKey(ex => ex.Id);

        builder
            .Property(ex => ex.SendDateTime)
            .IsRequired();

        builder
            .HasOne(ex => ex.Chat)
            .WithMany(ex => ex.Messages)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ex => ex.Sender)
            .WithMany(ex => ex.Messages)
            .OnDelete(DeleteBehavior.SetNull);

        var content = builder
            .OwnsOne(ex => ex.Content);

        content
            .Property(ex => ex.Content)
            .IsRequired();

        content
            .Property(ex => ex.ContentType)
            .IsRequired();

    }
}