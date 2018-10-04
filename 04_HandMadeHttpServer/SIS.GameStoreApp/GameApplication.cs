using System;
using GamesStoreData;
using Microsoft.EntityFrameworkCore;
using SIS.GameStoreApp.Controllers;
using SIS.Http.HTTP.Contracts;
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
                "/admin-games",
                req => new GameController().AdminGames(req.Session)
            );

            appRouteConfig.Get(
                "/add-game",
                req => new GameController().AddGame(req.Session)
            );

            appRouteConfig.Post(
                "/add-game",
                req => new GameController().AddGame(req)
            );

            appRouteConfig.Get(
                "/edit-game/{(?<id>[0-9]+)}",
                req => new GameController().EditGame(req.Session,req.UrlParameters["id"])
            );

            appRouteConfig.Post(
                "/edit-game/{(?<id>[0-9]+)}",
                req => new GameController().EditGame(req.FormData,req.UrlParameters)
            );
        }

        public void InitializeDatabase()
        {
            using (var db = new GameStoreDbContext())
            {
                db.Database.Migrate();
            }
        }
    }
}

