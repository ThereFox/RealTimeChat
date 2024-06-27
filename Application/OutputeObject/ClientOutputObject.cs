using Domain.ValueObjects;

namespace Application.OutputeObject;

public class ClientOutputObject
{
    public string NickName { get; }
    public UserRole Role { get; }
    
    protected ClientOutputObject(string nickName, UserRole role)
    {
        NickName = nickName;
        Role = role;
    }
    
    public static ClientOutputObject
    
}