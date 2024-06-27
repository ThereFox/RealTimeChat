using Domain;
using Domain.ValueObjects;

namespace Application.OutputeObject;

public class ClientOutputObject
{
    public Guid Id { get; }
    public string NickName { get; }
    public UserRole Role { get; }
    
    protected ClientOutputObject(Guid id, string nickName, UserRole role)
    {
        Id = id;
        NickName = nickName;
        Role = role;
    }

    public static ClientOutputObject FromDomain(ChatMemberAccount account)
    {
        return new ClientOutputObject(account.Owner.Id, account.Owner.Name, account.Role);
    }
    
}