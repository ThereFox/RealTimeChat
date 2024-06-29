using CSharpFunctionalExtensions;
using Domain;

namespace Application.Interfaces;

public interface IMessagesStore
{
    public Task<List<SendedMessage>> GetByChatWithPagination(Guid chat, int page, int pageSize);
    public Task<Result<int>> GetCount(Guid chatId);
    public Task<Result<SendedMessage>> GetById(Guid messageId);
    public Task<ChatMemberAccount> GetOwner(Guid messageId);
    
    public Task<Result<Guid>> Create(SendedMessage message);
    public Task<Result> Delete(Guid messageId);

}