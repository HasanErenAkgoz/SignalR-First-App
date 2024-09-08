using Microsoft.AspNetCore.SignalR;
using SignalR.Interfaces;

namespace SignalR.Hubs
{
    public class MyHub : Hub<IMessageClient>
    {
        static List<string> clients = new List<string>();
        public async Task SendMessageAsync(string message)
        {
        }

        public override async Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId);
            await Clients.All.GetClientsAsync(clients);
            await Clients.All.UserJoinedAsync(Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clients.Remove(Context.ConnectionId);
            await Clients.All.GetClientsAsync(clients);
            await Clients.All.UserLeavedAsync(Context.ConnectionId);

        }
    }

}
