using HandMadeHttpServer.Server.Enums;
using HandMadeHttpServer.Server.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Routing.Contracts
{
    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes
        {
            get;
        }

        void AddRoute(string route, RequestHandler httpHandler); 
    }
}
