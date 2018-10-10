using SIS.MvcFramework;
using SIS.WebServer;
using SIS.WebServer.Routing;

namespace SIS.CakesApp
{
    public static class WebHost
    {
       public static void Start(IMvcApplication application)
        {
            application.ConfigureServices();

            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            application.Configure(serverRoutingTable);

            Server server = new Server(8230, serverRoutingTable);
            server.Run();
        }
    }
}