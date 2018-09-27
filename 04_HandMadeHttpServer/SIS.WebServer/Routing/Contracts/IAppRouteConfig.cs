using System;
using System.Collections.Generic;
using SIS.HTTP.Enums;
using SIS.HTTP.HTTP.Contracts;
using SIS.WebServer.Handlers;

namespace SIS.WebServer.Routing.Contracts
{
    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes
        {
            get;
        }

        void Get(string route, Func<IHttpRequest, IHttpResponse> handlingFunc);

        void Post(string route, Func<IHttpRequest, IHttpResponse> handlingFunc);

        IReadOnlyList<string> AnonymousPaths { get; }

        void AddRoute(string route, HttpRequestMethod method, RequestHandler handler);

        void AddAnonymousPath(string path);

    }
}
