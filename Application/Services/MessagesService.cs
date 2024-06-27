using Application.Interfaces;
using CSharpFunctionalExtensions;
using Domain;

namespace Application.Services;

public class MessagesService
{
    private readonly IMessagesStore _messagesStorestore;
    private readonly IChatStore _chatStore;

    public async Task<Result<List<SendedMessage>>> GetMessagesByChatWithPagination(Guid chatId, int pageNumber, int pageSize)
    {
        if (pageNumber < 0 || pageSize <= 0)
        {
            throw new ArgumentException("invalid pagination settings");
        }

        if ((await _chatStore.Contain(chatId)).IsFailure)
        {
            return Result.Failure<List<SendedMessage>>("Chat dont have exist");
        }

        return await _messagesStorestore.GetWithPagination(chatId, pageNumber, pageSize);
    }
    
    public Result CreateMessage()
    {
        
    }
    
}