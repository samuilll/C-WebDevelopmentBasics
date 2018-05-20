using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server
{
    using Contracts;
    using HandMadeHttpServer.Server.Routing;
    using HandMadeHttpServer.Server.Routing.Contracts;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class WebServer : IRunnable
    {
        private readonly int port;
        private readonly IServerRouteConfig serverRouteConfig;
        private readonly TcpListener tcpListener;
        private bool isRunnig;

        public WebServer(int port, IAppRouteConfig routeConfig)
        {
            this.port = port;
            this.tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);

            this.serverRouteConfig = new ServerRouteConfig(routeConfig);
        }

        public void Run()
        {
            this.tcpListener.Start();
            this.isRunnig = true;

            Console.WriteLine($"Server started. Listening to TCP clients at 127.0.0.1:{port}");

            Task task = Task.Run(this.ListenLoop);
            task.Wait();
        }

        private async Task ListenLoop()
        {
            while (this.isRunnig)
            {
                Socket client = await this.tcpListener.AcceptSocketAsync();
                ConnectionHandler connetcionHandler = new ConnectionHandler(client,this.serverRouteConfig);
                Task connection = connetcionHandler.ProcessRequestAsync();
                connection.Wait();
            }
        }
    }
}
