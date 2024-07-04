using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Domain;

public class SendedMessage : Entity<Guid>
{
        
        public ChatMemberAccount Sender { get; }
        public Chat Chat { get; }
        public MessageContent Content { get; }
        public DateTime SendDateTime { get; }

        public static Result<SendedMessage> Create(
            Guid id,
            ChatMemberAccount sender,
            MessageContent message,
            DateTime sendDateTime
        )
        {
            if (sendDateTime > DateTime.Now)
            {
                return Result.Failure<SendedMessage>("Invalid date time");
            }

            return Result.Success(new SendedMessage(id, sender, message, sendDateTime));
        }
        
        protected SendedMessage(
            Guid id,
            ChatMemberAccount sender,
            MessageContent message,
            DateTime sendDateTime
            )
        {
            Id = id;
            Sender = sender;
            Content = message;
            SendDateTime = sendDateTime;
        }
}