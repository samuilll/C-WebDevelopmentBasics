﻿using HandMadeHttpServer.Server.Contracts;
using HandMadeHttpServer.Server.Enums;
using HandMadeHttpServer.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using HandMadeHttpServer.Server.HTTP.Response;
using HandMadeHttpServer.Server.Handlers;
using HandMadeHttpServer.ByTheCakeApplication.Controllers.Home;

namespace HandMadeHttpServer.ByTheCakeApplication
{
    public class CakeApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get("/",req=>new HomeController().Index());

            appRouteConfig.Get("/about", req => new HomeController().About());

            appRouteConfig.Get("/add",req=>new CakeController().Add());

            appRouteConfig.Post("/add", req => new CakeController().Add(req.FormData["name"],req.FormData["price"]));

            appRouteConfig.Get("/search", req => new CakeController().Search(req.UrlParameters));

        }
    }
}