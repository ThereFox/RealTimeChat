using CSharpFunctionalExtensions;
using Domain;

namespace Application.Interfaces;

public interface ICacheStore
{
    public Result ContainToken(string token);
    public Result AddAuntificatedUser(string token, Client client);
    
}