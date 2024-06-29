using Application.Interfaces;
using CSharpFunctionalExtensions;
using Domain;

namespace Persistense.Stores;

public class MessageStore : IMessagesStore
{
    public Task<List<SendedMessage>> GetWithPagination(Guid chat, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<List<SendedMessage>> GetByChatWithPagination(Guid chat, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<Result<int>> GetCount(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SendedMessage>> GetById(Guid messageId)
    {
        throw new NotImplementedException();
    }

    public Task<ChatMemberAccount> GetOwner(Guid messageId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Guid>> Create(SendedMessage message)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Delete(Guid messageId)
    {
        throw new NotImplementedException();
    }
}