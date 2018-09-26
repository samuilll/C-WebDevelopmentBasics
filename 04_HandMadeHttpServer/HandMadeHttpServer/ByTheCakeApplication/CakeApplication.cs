using HandMadeHttpServer.Server.Contracts;
using HandMadeHttpServer.Server.Enums;
using HandMadeHttpServer.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using HandMadeHttpServer.Server.HTTP.Response;
using HandMadeHttpServer.Server.Handlers;
using HandMadeHttpServer.ByTheCakeApplication.Controllers.Home;
using HandMadeHttpServer.ByTheCakeApplication.Controllers;
using Microsoft.EntityFrameworkCore;

namespace HandMadeHttpServer.ByTheCakeApplication
{
    using ViewModels;

    public class CakeApplication : IApplication
    {

        public void InitializeDatabase()
        {
            using (var db = new ShoppingDbContext())
            {
                db.Database.Migrate();
            }
        }
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .AddAnonymousPath("/");

            appRouteConfig
                .AddAnonymousPath("/login");

            appRouteConfig
                .AddAnonymousPath("/register");

            appRouteConfig
                .AddAnonymousPath("/successfulReg");

            appRouteConfig.Get("/",req=>new HomeController().Index());

            appRouteConfig
                .Get(
                "/about",
                 req => new HomeController().About()
                );

            appRouteConfig
                .Get(
                "/add",
                req=>new CakeController().Add()
                );

            appRouteConfig
                .Post(
                "/add",
                req => new CakeController()
                .Add(
                    req.FormData["name"],
                    req.FormData["price"]
                    ));

            appRouteConfig.Get("/search", req => new CakeController().Search(req));

            appRouteConfig.Get("/login", req => new AccountController().Login());

            appRouteConfig.Post("/login", req => new AccountController().Login(req));

            appRouteConfig
                .Get(
                "/shopping/add/{(?<id>[0-9]+)}",
                req => new ShoppingController().AddToCart(req)
                );

            appRouteConfig.Get("/cart", req => new ShoppingController().ShowCart(req));

            appRouteConfig.Get("/success", req => new ShoppingController().Success(req));

            appRouteConfig.Get("/logout", req => new AccountController().Logout(req));

            appRouteConfig.Get("/register", req => new AccountController().Register());

            appRouteConfig
                .Post(
                "/register",
                req => new AccountController().Register(new RegisterUserViewModel()
                { 
                     Username = req.FormData["username"],
                     Password = req.FormData["password"],
                     ConfirmPassword = req.FormData["passwordConfirmed"]
                }));
        }
    }
}
