using Microsoft.AspNetCore.SignalR;
using Domain.Entities;

namespace MessageServer.Hubs;

public class MessageHub : Hub
{
    public async Task SendMessage(Message message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
