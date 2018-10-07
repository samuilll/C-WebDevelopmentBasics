using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SIS.Http.Common;
using SIS.Http.Enums;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.WebServer.Handlers;
using SIS.WebServer.Routing.Contracts;

namespace SIS.WebServer
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequestAsync()
        {
                string httpRequest = await this.ReadRequest();

            if (httpRequest != null)
            {
                HttpContext httpContext = new HttpContext(httpRequest);

                if (!(httpContext.Request.RequestMethod==HttpRequestMethod.POST && httpContext.Request.FormData.Count==0))
                {
                    IHttpResponse httpResponse = new HttpHandler(this.serverRouteConfig).Handle(httpContext);

                    byte[] responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());

                    ArraySegment<byte> byteSegments = new ArraySegment<byte>(responseBytes);

                    await this.client.SendAsync(byteSegments, SocketFlags.None);

                    Console.WriteLine($"-----REQUEST-----");
                    Console.WriteLine(httpRequest);
                    Console.WriteLine($"-----RESPONSE-----");
                    Console.WriteLine(httpResponse.ToBaseString());
                    Console.WriteLine();
                }

            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<string> ReadRequest()
        {
            StringBuilder result = new StringBuilder();

            ArraySegment<byte> data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                string bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);

                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return result.ToString();
        }
    }
}
