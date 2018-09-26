using HandMadeHttpServer.Application;
using HandMadeHttpServer.ByTheCakeApplication;
using HandMadeHttpServer.Server;
using HandMadeHttpServer.Server.Contracts;
using HandMadeHttpServer.Server.Routing;
using HandMadeHttpServer.Server.Routing.Contracts;
using System;
using System.Collections.Generic;

namespace HandMadeHttpServer
{
  public  class Launcher:IRunnable
    {
        private WebServer webServer;
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

            this.webServer = new WebServer(8230, routeConfig);
            this.webServer.Run();
        }
    }
}
