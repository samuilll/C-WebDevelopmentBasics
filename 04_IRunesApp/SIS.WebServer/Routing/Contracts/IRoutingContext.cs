using System.Collections.Generic;
using SIS.WebServer.Handlers;

namespace SIS.WebServer.Routing.Contracts
{
   public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        RequestHandler RequestHandler { get; }
    }
}
