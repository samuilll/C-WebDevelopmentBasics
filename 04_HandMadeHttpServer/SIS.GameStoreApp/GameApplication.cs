﻿using System;
using SIS.GameStoreApp.Controllers;
using SIS.WebServer.Contracts;
using SIS.WebServer.Routing.Contracts;

namespace SIS.GameStoreApp
{
    public class GameApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddAnonymousPath("/");

            appRouteConfig.AddAnonymousPath("/login");

            appRouteConfig.AddAnonymousPath("/register");


            appRouteConfig.Get(
                "/login",
              req => new AccountController().Login(req.Session)
                );

            appRouteConfig.Post(
                "/login",
                req => new AccountController().Login(req.Session, req.FormData)
            );

            appRouteConfig.Get(
                "/",
                req => new HomeController().Index(req)
            );

            appRouteConfig.Get(
               "/register",
             req => new AccountController().Register(req.Session)
               );

            appRouteConfig.Post(
                "/register",
                req => new AccountController().Register(req.FormData)
            );

            appRouteConfig.Get(
                "/logout",
                req => new AccountController().Logout(req)
            );

            appRouteConfig.Get(
                "/all-games",
                req => new GameController().AllGames(req.Session)
            );

            appRouteConfig.Get(
                "/add-game",
                req => new GameController().AddGame(req.Session)
            );

            appRouteConfig.Post(
                "/add-game",
                req => new GameController().AddGame(req)
            );
        }
    }
}
