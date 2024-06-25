using Microsoft.AspNetCore.SignalR;
using SignalRTest.MessageDatas;

namespace SignalRTest
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            var UserName = Context?.User?.Identity?.Name ?? "Uncnow";
            await this.Clients.All.SendAsync("Receive", new MessageData(UserName, message));
        }
    }
}
