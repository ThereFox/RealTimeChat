using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class MessageContentType : ValueObject
{
    public static MessageContentType Text => new MessageContentType(0);
    public static MessageContentType File => new MessageContentType(1);
    protected static List<MessageContentType> _all = [Text, File];
    
    public int Value { get; }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Maybe<MessageContentType> Create(int Value)
    {
        if (_all.Any(ex => ex.Value == Value) == false)
        {
            return Maybe.None;
        }

        return _all.First(ex => ex.Value == Value).AsMaybe();
    }
    
    private MessageContentType(int value)
    {
        Value = value;
    }
}