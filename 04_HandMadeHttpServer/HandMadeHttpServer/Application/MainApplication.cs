using HandMadeHttpServer.Application.Controllers;
using HandMadeHttpServer.Server.Contracts;
using HandMadeHttpServer.Server.Handlers;
using HandMadeHttpServer.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Application
{
    class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get("/", req => new HomeController().Index());
            appRouteConfig.Get("/register", req => new UserController().RegisterGet());
            appRouteConfig.Post("/register", req => new UserController().RegisterPost(req.FormData["first-name"], req.FormData["middle-name"], req.FormData["last-name"]));
            appRouteConfig.Get("/user", req => new UserController().Details(req.UrlParameters["first-name"], req.UrlParameters["middle-name"], req.UrlParameters["last-name"]));
        }
    }
}
