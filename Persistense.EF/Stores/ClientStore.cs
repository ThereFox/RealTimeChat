using Application.Interfaces;
using CSharpFunctionalExtensions;
using Domain;

namespace Persistense.Stores;

public class ClientStore : IClientsStore
{
    public Task<Result<List<Client>>> GetAllWithPagination(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Client>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Create(Client client)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateNickName(Guid clientId, string newNickname)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Delete(Guid clientId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> JoinToChat(Guid clientId, Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> LeaveToChat(Guid clientId, Guid chatId)
    {
        throw new NotImplementedException();
    }
}