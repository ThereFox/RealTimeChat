using Application.InputObjects;
using Application.Interfaces;
using CSharpFunctionalExtensions;
using Domain;
using Domain.ValueObjects;

namespace Application.Services;

public class MessagesService
{
    private readonly IMessagesStore _messagesStore;
    private readonly IChatStore _chatStore;
    private readonly IClientsStore _clientsStore;
    private readonly IFileStore _fileStore;

    public MessagesService(
        IMessagesStore messagesStore,
        IChatStore chatStore,
        IClientsStore clientsStore,
        IFileStore fileStore)
    {
        _messagesStore = messagesStore;
        _chatStore = chatStore;
        _clientsStore = clientsStore;
        _fileStore = fileStore;
    }
    
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

        return await _messagesStore.GetByChatWithPagination(chatId, pageNumber, pageSize);
    }
    
    public async Task<Result<Guid>> CreateMessage(CreateMessageInputObject input, CancellationToken token)
    {
        var id = Guid.NewGuid();
        var getClientResult = await _clientsStore.GetById(input.ClientId);

        if (getClientResult.IsFailure)
        {
            return getClientResult.ConvertFailure<Guid>();
        }

        var getAccountResult = getChatAccount(getClientResult.Value, input.ChatId);

        if (getAccountResult.IsFailure)
        {
            return getAccountResult.ConvertFailure<Guid>();;
        }

        if ((await IsMessageValid(input.message, token)).IsFailure)
        {
            return  Result.Failure<Guid>("message invalid");
        }
        
        
        var createMessageResult = SendedMessage.Create(
            Guid.NewGuid(),
            getAccountResult.Value,
            input.message,
            DateTime.Now
            );

        if (createMessageResult.IsFailure)
        {
            return createMessageResult.ConvertFailure<Guid>();
        }


        var saveMessageResult = await _messagesStore.Create(createMessageResult.Value);

        return saveMessageResult;
    }
    private Result<ChatMemberAccount> getChatAccount(Client client, Guid chatId)
    {
        var chat = client
            .Accounts
            .FirstOrDefault(ex => ex.Chat.Id == chatId);

        return Result.SuccessIf(chat != null, chat!, "client dont in chat");
    }
    private async Task<Result> IsMessageValid(MessageContent content, CancellationToken token)
    {
        if (content.ContentType == MessageContentType.File)
        {
            return await _fileStore.Contain(content.Content, token);
        }

        return Result.Success();
    }
}