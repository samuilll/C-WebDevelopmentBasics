using Microsoft.EntityFrameworkCore;
using SIS.ByTheCakeApp.Controllers;
using SIS.ByTheCakeApp.ViewModels;
using SIS.WebServer.Contracts;
using SIS.WebServer.Routing.Contracts;

namespace SIS.ByTheCakeApp
{
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

            appRouteConfig.Get("/",req=>new HomeController().Index(req));

            appRouteConfig
                .Get(
                "/about",
                 req => new HomeController().About()
                );

            appRouteConfig
                .Get(
                "/add",
                req=>new ProductController().Add()
                );

            appRouteConfig
                .Post(
                "/add",
                req => new ProductController()
                .Add(
                    req.FormData["name"],
                    req.FormData["price"],
                    req.FormData["url"]
                    ));

            appRouteConfig.Get("/search", req => new ProductController().Search(req));

            appRouteConfig.Get("/login", req => new AccountController().Login(req));

            appRouteConfig.Post("/login", req => new AccountController().Login(req,new LoginUserViewModel()
                {
                    Username = req.FormData["username"],
                    Password = req.FormData["password"]
                }
            ));

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

            appRouteConfig.Get("/profile", req => new AccountController().ProfileView(req));

        }

    }
}
