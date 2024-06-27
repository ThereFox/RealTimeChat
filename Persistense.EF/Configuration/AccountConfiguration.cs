using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<ChatMemberAccount>
{
    public void Configure(EntityTypeBuilder<ChatMemberAccount> builder)
    {
        builder
            .HasKey(ex => ex.Id);

        builder
            .OwnsOne(ex => ex.Role)
            .Property(ex => ex.Value)
            .IsRequired();

        builder
            .HasOne(ex => ex.Chat)
            .WithMany(ex => ex.Members)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ex => ex.Owner)
            .WithMany(ex => ex.Accounts)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(ex => ex.Messages)
            .WithOne(ex => ex.Sender)
            .OnDelete(DeleteBehavior.SetNull);

    }
}