using SIS.MvcFramework;
using SIS.MvcFramework.Routers;
using SIS.WebServer;
using System;

namespace MvcDemo
{
    class Launcher
    {
        static void Main(string[] args)
        {
            Server server = new Server(8230, new ControllerRouter());

            MvcEngine.Run(server);
        }
    }
}
