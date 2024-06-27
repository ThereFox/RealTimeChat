using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Domain;

public class SendedMessage : Entity<Guid>
{
        
        public ChatMemberAccount Sender { get; }
        public MessageContent Content { get; }
        public DateTime SendDateTime { get; }

        public static Maybe<SendedMessage> Create(
            Guid id,
            ChatMemberAccount sender,
            MessageContent message,
            DateTime sendDateTime
        )
        {
            if (sendDateTime > DateTime.Now)
            {
                return Maybe.None;
            }

            return Maybe.From(new SendedMessage(id, sender, message, sendDateTime));
        }
        
        protected SendedMessage(
            Guid id,
            ChatMemberAccount sender,
            MessageContent message,
            DateTime sendDateTime
            )
        {
            Id = id;
            SenderName = sender;
            Content = message;
            SendDateTime = sendDateTime;
        }
}