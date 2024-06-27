using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Domain.Politics;

public class SendMessagePolicy
{
    private const int MaxMessagesOnDayPerBaseUser = 10;
    private const int MaxFilesOnDayPerBaseUser = 3;
    
    public Result CanSendMessage(ChatMemberAccount account, SendedMessage message)
    {
        if (account.Role == UserRole.Reader)
        {
            return Result.Failure("Reader cannot send messages");
        }

        if (countOfSendedMessagesInAvaliableDiapason(account, message) == false)
        {
            return Result.Failure("Day limit was exhausted");
        }

        return Result.Success();
    }

    private bool countOfSendedMessagesInAvaliableDiapason(
        ChatMemberAccount account,
        SendedMessage newMessage
    )
    {
        if (account.Role == UserRole.Admin)
        {
            return true;
        }

        if (getCountOfMessagesByCurrentDate(account) + 1 >= MaxMessagesOnDayPerBaseUser)
        {
            return false;
        }
        
        if (isFile(newMessage) && getCountOfSendedFileByCurrentDate(account) + 1 >= MaxFilesOnDayPerBaseUser)
        {
            return false;
        }

        return true;
    }

    private int getCountOfMessagesByCurrentDate(ChatMemberAccount account)
    {
        return account.Messages
            .Where(ex => ex.SendDateTime >= DateTime.Today)
            .Count();
    }

    private int getCountOfSendedFileByCurrentDate(ChatMemberAccount account)
    {
        return account.Messages
            .Where(ex => ex.SendDateTime >= DateTime.Today)
            .Where(ex => ex.Content.ContentType == MessageContentType.File)
            .Count();
    }

    private bool isFile(SendedMessage message)
    {
        return message.Content.ContentType == MessageContentType.File;
    }
}