using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class MessageContent : ValueObject
{
    public string Content { get; }
    public MessageContentType ContentType { get; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return ContentType.Value;
        yield return Content;
    }

    protected MessageContent(string content, MessageContentType contentType)
    {
        Content = content;
        contentType = contentType;
    }

    public static Maybe<MessageContent> Create(string content, MessageContentType contentType)
    {
        if (string.IsNullOrWhiteSpace(content) == false)
        {
            return Maybe.None;
        }

        return Maybe.From(new MessageContent(content, contentType));
    }
}