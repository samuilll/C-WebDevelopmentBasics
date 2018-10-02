using Microsoft.EntityFrameworkCore;
using SIS.ByTheCakeApp.Controllers;
using SIS.ByTheCakeData;
using SIS.ByTheCakeData.ViewModels;
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
                 req => new HomeController().About(req)
                );

            appRouteConfig
                .Get(
                "/add",
                req=>new ProductController().Add(req)
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
                req => new ShoppingController().AddToOrder(req)
                );

            appRouteConfig
                .Get(
                "/product/details/{(?<id>[0-9]+)}",
                req => new ProductController().Details(req)
                );

            appRouteConfig
               .Get(
               "/product/partOfOrderDetails/{(?<id>[0-9]+)}",
               req => new ProductController().OrderProductDetails(req)
               );

            appRouteConfig.Get("/order", req => new ShoppingController().ShowCurrentOrder(req));

            appRouteConfig.Get("/order/{(?<id>[0-9]+)}", req => new ShoppingController().ShowCompleteOrder(req));

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

            appRouteConfig.Get("/myOrders", req => new AccountController().OrdersView(req));

        }

    }
}
