using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Routing
{
    using Contracts;
    using HandMadeHttpServer.Server.Enums;
    using HandMadeHttpServer.Server.Handlers;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class ServerRouteConfig : IServerRouteConfig
    {
        private readonly IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> routes;

        public  IAppRouteConfig AppRouteConfig { get;}

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();

            this.AppRouteConfig = appRouteConfig;

            var availableMethods = Enum
              .GetValues(typeof(HttpRequestMethod))
              .Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, IRoutingContext>();
            }

            this.InitializeServerConfig(appRouteConfig);
        }

        public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes => this.routes;


        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (KeyValuePair<HttpRequestMethod, Dictionary<string, RequestHandler>> kvp in appRouteConfig.Routes)
            {
                foreach (KeyValuePair<string, RequestHandler> requestHandler in kvp.Value)
                {
                    List<string> args = new List<string>();

                    string parsedRegex = this.ParseRoute(requestHandler.Key, args);

                    IRoutingContext routingContext = new RoutingContext(args, requestHandler.Value);

                    this.Routes[kvp.Key].Add(parsedRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string requestHandlerKey, List<string> args)
        {
            var parsedRegex = new StringBuilder();
            parsedRegex.Append("^");

            if (requestHandlerKey == "/")
            {
                parsedRegex.Append("/$");
                return parsedRegex.ToString();
            }

            var tokens = requestHandlerKey.Split(new[] { '/' });

            this.ParseTokens(tokens, args, parsedRegex);

            return parsedRegex.ToString();
        }

        private void ParseTokens(string[] tokens, List<string> parameters, StringBuilder result)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                var end = tokens.Length - 1 == i ? "$" : "/";
                var currentToken = tokens[i];

                if (!currentToken.StartsWith('{') && !currentToken.EndsWith('}'))
                {
                    result.Append($"{currentToken}{end}");
                    continue;
                }

                var parameterRegex = new Regex("<\\w+>");
                var parameterMatch = parameterRegex.Match(currentToken);

                if (!parameterMatch.Success)
                {
                    throw new InvalidOperationException($"Route parameter in {currentToken} not valid");
                }

                var match = parameterMatch.Value;
                var parameter = match.Substring(1,match.Length-2);

                parameters.Add(parameter);

                var currentTokenWithoutCurlyBrackets = currentToken.Substring(1,currentToken.Length-2);

                result.Append($"{currentTokenWithoutCurlyBrackets}{end}");
            }
        }


    }


}
