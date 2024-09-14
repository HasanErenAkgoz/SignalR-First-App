using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs
{
    public class MessageHub : Hub
    {
        //public async Task SendMessageAsync(string message, IEnumerable<string> connectionIds)
        //{

        //    #region Caller
        //    //Communicates only with the client sending notifications to the server
        //    //await Clients.Caller.SendAsync("receiveMessage",message);
        //    #endregion

        //    #region All
        //    // Communicates with all clients connected to the server
        //    //await Clients.All.SendAsync("receiveMessage", message);
        //    #endregion

        //    #region Other
        //    // Sends messages to all clients connected to the server except the client that only sends notifications to the server
        //    //await Clients.Others.SendAsync("receiveMessage",message);
        //    #endregion

        //    #region AllExcept
        //    // Notifies all clients connected to the server except the specified clients
        //    //await Clients.AllExcept(connectionIds).SendAsync("reciveMessages",message);
        //    #endregion

        //    #region Clients
        //    //Requests only the specified client among the clients connected to the server
        //    await Clients.Clients(connectionIds).SendAsync("reciveMessages", message);
        //    #endregion



        //}
        public async Task SendMessageAsync(string message, string groupName)
        {
            #region Clients
            //Requests only the specified client among the clients connected to the server
            await Clients.Group(groupName).SendAsync("receiveMessage", message);
            #endregion



        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("getConnectionId", Context.ConnectionId);
        }

        public async Task AddGroup(string connectionId,string groupName)
        {
           await Groups.AddToGroupAsync(connectionId, groupName);
        }
    }
}
