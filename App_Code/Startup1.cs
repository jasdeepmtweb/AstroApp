using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AstroApp.App_Code.Startup1))]

namespace AstroApp.App_Code
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null; // Unlimited
        }
    }
}
