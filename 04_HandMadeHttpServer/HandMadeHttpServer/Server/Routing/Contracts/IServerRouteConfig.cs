using HandMadeHttpServer.Server.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Routing.Contracts
{
   public interface IServerRouteConfig
    {
        IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }

        IAppRouteConfig AppRouteConfig { get; }
    }
}
