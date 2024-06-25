namespace Domain.ValueObject;

public class UserRole
{
    public static UserRole Admin => new UserRole(0);
    public static UserRole Reader => new UserRole(0);
    public static UserRole Common => new UserRole(0);
    
    public int Value { get; }

    protected UserRole(int value)
    {
        Value = value;
    }
    
}