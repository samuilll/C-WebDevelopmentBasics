using System;
using SIS.GameStoreApp.Controllers;
using SIS.WebServer.Contracts;
using SIS.WebServer.Routing.Contracts;

namespace SIS.GameStoreApp
{
    public class GameApplication:IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddAnonymousPath("/login");

            appRouteConfig.AddAnonymousPath("/register");


            appRouteConfig.Get(
                "/login",
              req=>  new AccountController().Login()
                );

            appRouteConfig.Get(
               "/register",
             req => new AccountController().Register()
               );
        }
    }
}
