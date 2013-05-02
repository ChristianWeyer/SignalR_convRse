using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ConvRse.Services
{
    [HubName("monitor")]
    public class MonitorHub : Hub
    {
        public void PublishEvent(string eventType, string connection)
        {
            Clients.All.newEvent(eventType, connection);
        }
    }
}
