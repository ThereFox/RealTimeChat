using Application.Interfaces;
using CSharpFunctionalExtensions;
using Domain;

namespace Infrastructure;

public class CacheStore : ICacheStore
{
    public Result ContainToken(string token)
    {
        throw new NotImplementedException();
    }

    public Result AddAuntificatedUser(string token, Client client)
    {
        throw new NotImplementedException();
    }
}