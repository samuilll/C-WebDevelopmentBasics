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
    using HandMadeHttpServer.Server.HTTP;

    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            try
            {
                var currentPath = context.Request.Path;
                var loginPath = "/login";

                if (currentPath != loginPath && !context.Request.Session.Contains(SessionStore.CurrentUserKey))
                {
                    return new RedirectResponse(loginPath);
                }

                var method = context.Request.RequestMethod;
                var registeredRoutes = this.serverRouteConfig.Routes[method];

                foreach (var registeredRoute in registeredRoutes)
                {
                    string pattern = registeredRoute.Key;
                    var routingContext = registeredRoute.Value;
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(context.Request.Path);

                    if (!match.Success)
                    {
                        continue;
                    }

                    foreach (var parameter in routingContext.Parameters)
                    {
                        context.Request.AddUrlParameter(parameter, match.Groups[parameter].Value);
                    }

                    return registeredRoute.Value.RequestHandler.Handle(context);
                }
            }
            catch (Exception ex)
            {
                return new InternalServerErrorResponse(ex);
            }

            return new NotFoundResponse();
        }
    }
}
