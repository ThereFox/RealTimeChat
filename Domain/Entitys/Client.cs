using CSharpFunctionalExtensions;

namespace Domain;

public class Client : Entity<Guid>
{
    public string Name { get; set; }
    public IReadOnlyCollection<ChatMemberAccount> Accounts { get; }

    protected Client(Guid id, string name, List<ChatMemberAccount> accounts)
    {
        Id = id;
        Name = name;
        Accounts = accounts;
    }

    public static Maybe<Client> Create(Guid id, string name, List<ChatMemberAccount> accounts)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Maybe.None;
        }

        return Maybe.From(new Client(id, name, accounts));
    }
}