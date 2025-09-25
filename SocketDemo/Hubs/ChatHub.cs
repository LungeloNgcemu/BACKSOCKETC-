using Microsoft.AspNetCore.SignalR;

namespace SocketDemo.Hubs
{
    public class ChatHub : Hub
    {
        // Join a channel
        public async Task JoinChannel(string channelName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channelName);
           // await Clients.Group(channelName).SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} joined {channelName}");
        }

        // Leave a channel
        public async Task LeaveChannel(string channelName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelName);
            await Clients.Group(channelName).SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} left {channelName}");
        }

        // Send message to a specific channel
        public async Task SendMessageToChannel(string channelName, string user, string message)
        {
            await Clients.Group(channelName).SendAsync("ReceiveMessage", user, message);
        }
    }
}
