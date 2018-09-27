using System;
using System.Linq;
using System.Text.RegularExpressions;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;

using SIS.WebServer.Handlers.Contracts;
using SIS.WebServer.Routing.Contracts;

namespace SIS.WebServer.Handlers
{
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

                var anonymousPaths = this.serverRouteConfig.AppRouteConfig.AnonymousPaths.ToList();


                if (!anonymousPaths.Contains(currentPath) && !context.Request.Session.Contains(SessionStore.CurrentUserKey))
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
