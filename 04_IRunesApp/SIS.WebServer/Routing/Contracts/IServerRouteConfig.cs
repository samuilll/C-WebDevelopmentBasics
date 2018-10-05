using System.Collections.Generic;
using SIS.Http.Enums;

namespace SIS.WebServer.Routing.Contracts
{
   public interface IServerRouteConfig
    {
        IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }

        IAppRouteConfig AppRouteConfig { get; }
    }
}
