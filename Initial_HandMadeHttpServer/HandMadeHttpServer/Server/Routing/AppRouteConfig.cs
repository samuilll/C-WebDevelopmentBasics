using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Routing
{
    using Contracts;
    using HandMadeHttpServer.Server.Enums;
    using HandMadeHttpServer.Server.Handlers;
    using System.Linq;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> routes;
        public IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes => this.routes;

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

        public void AddRoute(string route, RequestHandler handler)
        {
            var handlerName = handler
                .GetType()
                .Name
                .ToLower();

            if (handlerName.Contains("get"))
            {
                this.routes[HttpRequestMethod.GET].Add(route, handler);
            }
            else if (handlerName.Contains("post"))
            {
                this.routes[HttpRequestMethod.POST].Add(route, handler);
            }
            else
            {
                throw new InvalidOperationException("Invalid handler!");
            }

        }
    }
}
