using CSharpFunctionalExtensions;

namespace Domain;

public class Chat : Entity<Guid>
{   
    public string Name { get; private set; }
    
    public IReadOnlyCollection<ChatMemberAccount> Members { get; }
    public IReadOnlyCollection<SendedMessage> Messages { get; }

    public static Maybe<Chat> Create(Guid id, string name, List<ChatMemberAccount> members, List<SendedMessage> sendedMessages)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Maybe.None;
        }

        return Maybe.From(new Chat(id, name, members, sendedMessages));
    }
    
    protected Chat(Guid id, string name, List<ChatMemberAccount> members, List<SendedMessage> sendedMessages)
    {
        Id = id;
        Name = name;
        Members = members;
        Messages = sendedMessages;
    }
}