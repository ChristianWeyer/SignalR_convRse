using ConvRse.Services;
using Microsoft.AspNet.SignalR;
using Owin;
using System.Reflection;

namespace ConvRse.ConsoleHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // web host uses BuildManagerAssemblyLocator, we need a manual way, somehow
            var a = Assembly.LoadFrom("ConvRse.Services.dll");
            
            app.MapConnection<ChatConnection>("/rawchat", new ConnectionConfiguration { EnableCrossDomain = true });
			app.MapHubs(new HubConfiguration { EnableCrossDomain = true } );
		}
    }
}
