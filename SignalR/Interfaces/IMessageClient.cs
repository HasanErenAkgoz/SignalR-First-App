namespace SignalR.Interfaces
{
    public interface IMessageClient
    {

        Task GetClientsAsync(List<string> clients);
        Task UserJoinedAsync(string connectionId);
        Task UserLeavedAsync(string connectionId);

    }
}
