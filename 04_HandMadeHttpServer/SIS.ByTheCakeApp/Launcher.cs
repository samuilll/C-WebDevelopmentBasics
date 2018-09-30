using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;
using SIS.WebServer;

namespace SIS.ByTheCakeApp
{
  public  class Launcher
    {
        private Server webServer;

      public  static void Main(string[] args)
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var app = new CakeApplication();

            app.InitializeDatabase();

            IAppRouteConfig routeConfig = new AppRouteConfig();

            app.Configure(routeConfig);

            this.webServer = new Server(8230, routeConfig);
            this.webServer.Run();
        }
    }
}
