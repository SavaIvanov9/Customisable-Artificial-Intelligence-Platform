namespace CAI.Web.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class Chat : Hub
    {
        public void SendMessege(string message)
        {
            var chatMessage = $"Message from {this.Context.ConnectionId}: {message}";
            //this.Clients.Caller.receiveMessage(message);
            this.Clients.All.receiveMessage(message);
        }
    }
}