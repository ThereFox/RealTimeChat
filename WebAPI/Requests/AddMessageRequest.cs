namespace SignalRTest.Requests;

public record AddMessageRequest
(
    string ChatId,
    MessageDTO message
);