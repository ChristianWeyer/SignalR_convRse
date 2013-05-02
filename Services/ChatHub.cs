using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace ConvRse.Services
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            var msg = string.Format(
                "{0}: {1}", Clients.Caller.foo, message);
            
            Clients.All.newMessage(msg);
        }

        public void JoinRoom(string room)
        {
            Groups.Add(Context.ConnectionId, room);
        }

        public void SendMessageForRoom(string room, string message)
        {
            var msg = string.Format("{0}: {1}", Context.ConnectionId, message);

            Clients.Group(room).newMessage(msg);
            //Clients.Group(room).newData(complexObject);
        }

        #region Lifecycle
        public override Task OnConnected()
        {
            SendMonitorData("Connected", Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            SendMonitorData("Disconnected", Context.ConnectionId);

            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            SendMonitorData("Reconnected", Context.ConnectionId);

            return base.OnReconnected();
        }

        private void SendMonitorData(string eventType, string connection)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MonitorHub>();
            context.Clients.All.newEvent(eventType, connection);
        } 
        #endregion
    }
}
