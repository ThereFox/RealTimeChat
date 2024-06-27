using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Domain;

public class ChatMemberAccount : Entity<Guid>
{
    
    public Client Owner { get; }
    public UserRole Role { get; private set; }
    public IReadOnlyCollection<SendedMessage> Messages { get; }
    public Chat Chat { get; }

    public static Maybe<ChatMemberAccount> Create(
        Guid id,
        Client owner,
        UserRole role,
        List<SendedMessage> messages,
        Chat chat)
    {
        return Maybe.From(new ChatMemberAccount(id, owner, role, messages, chat));
    }

    protected ChatMemberAccount(
        Guid id,
        Client owner,
        UserRole role,
        List<SendedMessage> messages,
        Chat chat
        )
    {
        Id = id;
        Owner = owner;
        Role = role;
        Messages = messages;
        Chat = chat;
    }
}