using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Handlers
{
    using Contracts;
    using Server.Routing.Contracts;
    using Server.HTTP.Contracts;
    using System.Text.RegularExpressions;
    using HandMadeHttpServer.Server.HTTP.Response;

    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            var method = httpContext.Request.RequestMethod;

            var routes = this.serverRouteConfig.Routes[method];

            foreach (var kvp in routes)
            {
                string pattern = kvp.Key;
                var routingContext = kvp.Value;
                Regex regex = new Regex(pattern);
                Match match = regex.Match(httpContext.Request.Path);

                if (!match.Success)
                {
                    continue;
                }

                foreach (var parameter in routingContext.Parameters)
                {
                    httpContext.Request.AddUrlParameter(parameter, match.Groups[parameter].Value);
                }

                return kvp.Value.RequestHandler.Handle(httpContext);
            }

            return new NotFoundResponse();
        }
    }
}
