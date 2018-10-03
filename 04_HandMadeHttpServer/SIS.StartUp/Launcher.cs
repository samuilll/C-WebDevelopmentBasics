using System;
using SIS.ByTheCakeApp;
using SIS.WebServer;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;
using SIS.GameStoreApp;

namespace SIS.StartUp
{
    public class Launcher
    {
        private Server webServer;

        public static void Main(string[] args)
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var app = new GameApplication();

           //app.InitializeDatabase();

            IAppRouteConfig routeConfig = new AppRouteConfig();

            app.Configure(routeConfig);

            this.webServer = new Server(8230, routeConfig);
            this.webServer.Run();
        }
    }
}
