using System;
using System.Collections.Generic;
using System.Linq;
using SIS.HTTP.Enums;
using SIS.HTTP.HTTP.Contracts;
using SIS.WebServer.Handlers;
using SIS.WebServer.Routing.Contracts;

namespace SIS.WebServer.Routing
{
    public class AppRouteConfig : IAppRouteConfig
    {

        private readonly Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> routes;

        public IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes => this.routes;

        private List<string> anonymousPaths =  new List<string>();

        public IReadOnlyList<string> AnonymousPaths
        {
            get
            {
                return this.anonymousPaths.AsReadOnly();
            }

        }
        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>>();

            var availableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, RequestHandler>();
            }
        }

        public void Get(string route,Func<IHttpRequest,IHttpResponse> handlingFunc)
        {
            this.AddRoute(route, HttpRequestMethod.GET, new RequestHandler(handlingFunc));
        }

        public void Post(string route, Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            this.AddRoute(route, HttpRequestMethod.POST, new RequestHandler(handlingFunc));
        }

        public void AddRoute(string route, HttpRequestMethod method,RequestHandler handler)
        {
            this.routes[method].Add(route,handler);
        }

        public void AddAnonymousPath(string path)
        {
            this.anonymousPaths.Add(path);
        }
    }
}
