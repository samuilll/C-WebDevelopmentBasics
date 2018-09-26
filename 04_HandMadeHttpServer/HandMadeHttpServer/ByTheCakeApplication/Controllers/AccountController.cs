using HandMadeHttpServer.ByTheCakeApplication.Models;
using HandMadeHttpServer.Infrastructure;
using HandMadeHttpServer.Server.HTTP;
using HandMadeHttpServer.Server.HTTP.Contracts;
using HandMadeHttpServer.Server.HTTP.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.ByTheCakeApplication.Controllers
{
  public  class AccountController:Controller
    {


        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["isAuthenticated"] = "none";

            return this.FileViewResponse("Account/login");
        }

        public IHttpResponse Login(IHttpRequest req)
        {
            var formNameKey = "name";
            var formPasswordKey = "password";

            if (!req.FormData.ContainsKey(formNameKey)
                || !req.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            var name = req.FormData[formNameKey];
            var password = req.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(name)
                || string.IsNullOrWhiteSpace(password))
            {

            this.ViewData["showError"] = "block";
            this.ViewData["error"] = "Please fill all the fields";

            return this.FileViewResponse("Account/login");
            }

            req.Session.Add(SessionStore.CurrentUserKey, name);
            req.Session.Add(SessionStore.ShoppingCardKey, new ShoppingCard());
            return new RedirectResponse("/");
        }

        internal IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }
    }
}
