using HandMadeHttpServer.Server.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Routing.Contracts
{
   public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        RequestHandler RequestHandler { get; }
    }
}
