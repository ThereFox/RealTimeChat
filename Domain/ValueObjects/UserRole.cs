using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class UserRole : ValueObject
{
    public static UserRole Common => new UserRole(0);
    public static UserRole Admin => new UserRole(1);
    public static UserRole Reader => new UserRole(2);

    protected static List<UserRole> AllAvaliable = [Admin, Reader, Common];
    
    public int Value { get; }

    public static Maybe<UserRole> Create(int value)
    {
        if (AllAvaliable.Any(ex => ex.Value == value) == false)
        {
            return Maybe<UserRole>.None;
        }

        return Maybe.From(AllAvaliable.First(ex => ex.Value == value));
    }
    
    protected UserRole(int value)
    {
        Value = value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}