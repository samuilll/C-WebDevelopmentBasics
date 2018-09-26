using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Task03_SimpleWebServer
{
    class Program
    {
        static void Main(string[] args)
        {

            int port = 1300;
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            TcpListener server = new TcpListener(ip, port);
            server.Start();

            Task task = Connect(server);

            task.Wait();
        }

         static async Task Connect(TcpListener listener)
        {
            while (true)
            {
                //wait for browser to connect
                var client =await listener.AcceptTcpClientAsync();

                var request = new byte[1024];
                await client.GetStream().ReadAsync(request, 0, request.Length);

                Console.WriteLine(Encoding.UTF8.GetString(request));

                byte[] data = Encoding.UTF8.GetBytes("Hello");
                await client.GetStream().WriteAsync(data,0,data.Length);

                client.GetStream().Dispose();
            }
        }
    }
}
