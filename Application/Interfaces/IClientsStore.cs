using CSharpFunctionalExtensions;
using Domain;

namespace Application.Interfaces;

public interface IClientsStore
{
    public Task<Result<List<Client>>> GetAllWithPagination(int page, int pageSize);
    public Task<Result<Client>> GetById(Guid id);
    public Task<Result> Create(Client client);
    public Task<Result> UpdateNickName(Guid clientId, string newNickname);
    public Task<Result> Delete(Guid clientId);

    public Task<Result> JoinToChat(Guid clientId, Guid chatId);
    public Task<Result> LeaveToChat(Guid clientId, Guid chatId);
}