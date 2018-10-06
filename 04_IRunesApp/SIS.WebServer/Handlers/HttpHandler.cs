using System;
using System.Linq;
using System.Text.RegularExpressions;
using SIS.Http.Enums;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;
using SIS.Infrastructure;
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

                var loginPath = "/Users/login";

                var anonymousPaths = this.serverRouteConfig.AppRouteConfig.AnonymousPaths.ToList();

                const string StylesFolder = "/styles";

                const string ScriptsFolder = "/scripts";

                const string CssFolder = "/css";

                const string JsFolder = "/js";



                var allowedFolders = new string[] { StylesFolder,ScriptsFolder,CssFolder,JsFolder};

                if (allowedFolders.Any(folder => currentPath.Contains(folder)))
                {
                    var extension = currentPath.Substring(currentPath.LastIndexOf('.')+1,currentPath.Length-currentPath.LastIndexOf('.')-1);

                    return new TextPlainResponse(HttpStatusCode.OK, new FileView(currentPath,extension));
                }

                if (!anonymousPaths.Contains(currentPath) && !context.Request.Session.Contains(SessionStore.CurrentUserKey))
                {
                    return new RedirectResponse(loginPath);
                }

                var method = context.Request.RequestMethod;
                var registeredRoutes = this.serverRouteConfig.Routes[method];

                foreach (var registeredRoute in registeredRoutes)
                {
                    string url = context.Request.Url;
                    string pattern = registeredRoute.Key;
                   
                    var routingContext = registeredRoute.Value;

                    if (pattern.Contains("?"))
                    {
                        pattern = ReplaceFirstLetterOnlyIfItsNotOnlyOne(pattern, "?", @"\?");
                    }

                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(url);

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

        
    
        private string ReplaceFirstLetterOnlyIfItsNotOnlyOne(string text, string textToReplace, string replace)
        {
            int pos = text.IndexOf(textToReplace);

            if (!text.Substring(pos+1,text.Length-pos-1).Contains(textToReplace))
            {
                return text;
            }
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + textToReplace.Length);
        }

    }
}
