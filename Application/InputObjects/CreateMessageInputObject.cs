using Domain.ValueObjects;

namespace Application.InputObjects;

public record CreateMessageInputObject
(
    Guid ClientId,
    Guid ChatId,
    MessageContent message
);