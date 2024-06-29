using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Domain;

public class Chat : Entity<Guid>
{   
    public string Name { get; private set; }
    public UserRole DefaultUserRole { get; private set; }
    
    public IReadOnlyCollection<ChatMemberAccount> Members { get; }
    public IReadOnlyCollection<SendedMessage> Messages { get; }

    public static Maybe<Chat> Create(Guid id, UserRole role, string name, List<ChatMemberAccount> members, List<SendedMessage> sendedMessages)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Maybe.None;
        }

        return Maybe.From(new Chat(id, role, name, members, sendedMessages));
    }
    
    protected Chat(Guid id, UserRole role, string name, List<ChatMemberAccount> members, List<SendedMessage> sendedMessages)
    {
        Id = id;
        DefaultUserRole = role;
        Name = name;
        Members = members;
        Messages = sendedMessages;
    }

    public Result UpdateName(string newName)
    {
        return Result.Success();
    }
    public Result UpdateDefaultUserRole(UserRole newDefaultRole)
    {
        if (newDefaultRole == UserRole.Admin)
        {
            return Result.Failure("default role cannot be admin");
        }

        if (newDefaultRole == DefaultUserRole)
        {
            return Result.Failure("nothing to update");
        }

        DefaultUserRole = newDefaultRole;
        
        return Result.Success();
    }
    
}