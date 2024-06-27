namespace Application.InputObjects;

public record CreateMessageInputObject
(
    Guid ChatId,
    string message
);