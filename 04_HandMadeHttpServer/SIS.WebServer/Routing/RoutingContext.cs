using System.Collections.Generic;
using SIS.HTTP.Common;
using SIS.WebServer.Handlers;
using SIS.WebServer.Routing.Contracts;

namespace SIS.WebServer.Routing
{
    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(IEnumerable<string> parameters, RequestHandler requestHandler)
        {
            CoreValidator.ThrowIfNull(requestHandler, nameof(requestHandler));

            this.Parameters = parameters;
            this.RequestHandler = requestHandler;
        }

        public IEnumerable<string> Parameters { get; }

        public RequestHandler RequestHandler { get; }
    }
}
