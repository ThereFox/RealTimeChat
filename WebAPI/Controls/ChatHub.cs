using Application.InputObjects;
using Application.Services;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRTest.Requests;

namespace SignalRTest
{
    public class ChatHub : Hub
    {
        private readonly Guid ClientIdHardCode = Guid.Parse("7f8f0942-7139-45c0-8bb5-2496edec0997");
        private readonly Guid ChatIdHardCode = Guid.Parse("");
        
        private readonly MessagesService _MessageService;
        //private readonly AuthService _authService;
        private readonly IHttpContextAccessor _acsessor;
        
        public async Task AddNewMessage(AddMessageRequest request, CancellationToken token)
        {
            if (Guid.TryParse(request.ChatId, out var chatId) == false)
            {
                return;
            }

            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, request.ChatId);

            
            var validateContentType = MessageContentType.Create(
                request.message.messageType
            );

            if (validateContentType.HasNoValue)
            {
                return;
            }
            
            var messageContent = MessageContent.Create(
                request.message.message,
                validateContentType.Value
                );

            if (messageContent.HasNoValue)
            {
                return;
            }

            var inputObject = new CreateMessageInputObject(
                ClientIdHardCode,
                chatId,
                messageContent.Value
            );
            
            var createMessageResult = await _MessageService.CreateMessage(inputObject, token);
            
            await this.Clients.Group(request.ChatId).SendAsync("Receive", inputObject);
        }
    }
}
