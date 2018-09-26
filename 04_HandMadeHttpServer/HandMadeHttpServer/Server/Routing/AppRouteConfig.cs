using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Routing
{
    using Contracts;
    using HandMadeHttpServer.Server.Enums;
    using HandMadeHttpServer.Server.Handlers;
    using HandMadeHttpServer.Server.HTTP.Contracts;
    using System.Linq;

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
