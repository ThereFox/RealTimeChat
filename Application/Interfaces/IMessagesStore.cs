using CSharpFunctionalExtensions;
using Domain;

namespace Application.Interfaces;

public interface IMessagesStore
{
    public Task<List<SendedMessage>> GetWithPagination(Guid chat, int page, int pageSize);
    public Task<ChatMemberAccount> GetOwner(Guid messageId);
    
    public Task<Result<Guid>> Create(SendedMessage message);
    public Task<Result> Delete(Guid messageId);

}