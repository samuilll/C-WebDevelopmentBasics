using IRunesApp;
using SIS.WebServer;
using SIS.WebServer.Contracts;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;

namespace StartUp
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
            IApplication app = new IRunesApplication();

            app.InitializeDatabase();

            IAppRouteConfig routeConfig = new AppRouteConfig();

            app.Configure(routeConfig);

            this.webServer = new Server(8230, routeConfig);
            this.webServer.Run();
        }
    }
}
