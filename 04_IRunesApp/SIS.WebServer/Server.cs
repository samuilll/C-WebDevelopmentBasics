using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SIS.WebServer.Contracts;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;


namespace SIS.WebServer
{
    public class Server : IRunnable
    {
        private readonly int port;
        private readonly IServerRouteConfig serverRouteConfig;
        private readonly TcpListener tcpListener;
        private bool isRunnig;

        public Server(int port, IAppRouteConfig routeConfig)
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
