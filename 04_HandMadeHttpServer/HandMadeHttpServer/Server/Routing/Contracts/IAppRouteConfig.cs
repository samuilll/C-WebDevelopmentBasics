using HandMadeHttpServer.Server.Enums;
using HandMadeHttpServer.Server.Handlers;
using HandMadeHttpServer.Server.HTTP.Contracts;
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

        void Get(string route, Func<IHttpRequest, IHttpResponse> handlingFunc);

        void Post(string route, Func<IHttpRequest, IHttpResponse> handlingFunc);

        void AddRoute(string route, HttpRequestMethod method, RequestHandler handler); 
    }
}
