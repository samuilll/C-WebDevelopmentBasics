using HandMadeHttpServer.Infrastructure;
using HandMadeHttpServer.Server.HTTP;
using HandMadeHttpServer.Server.HTTP.Contracts;
using HandMadeHttpServer.Server.HTTP.Response;

namespace HandMadeHttpServer.ByTheCakeApplication.Controllers
{
    using ByTheCakeApplication.Models;
    using ByTheCakeApplication.ViewModels;
    using ByTheCakeApplication.Services;
    using Services.Contracts;
    using System;

    public  class AccountController:Controller
    {
        private IUserService userService;

        public AccountController()
        {
            this.userService = new UserService();
        }
        public IHttpResponse Login()
        {
            SetAnonimoysView();
            SetWithoutErrorView();

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

        internal IHttpResponse Register()
        {
            SetAnonimoysView();
            SetWithoutErrorView();

            return this.FileViewResponse("Account/register");
        }

        internal IHttpResponse Register(RegisterUserViewModel model)
        {
            var username = model.Username;
            var password = model.Password;
            var confirmPassword = model.ConfirmPassword;

            if (username.Length<3
                || password.Length<3
                || confirmPassword != password)
            {

                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Invalid user parameters!";

                return this.FileViewResponse("Account/register");
            }

            var success = this.userService.Create(username, password);

            if (success)
            {
                this.ViewData["new-user"] = username;

                return this.FileViewResponse("Account/successfulReg");
            }

            else
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "This username is taken";

                return this.FileViewResponse("Account/register");

            }
        }


        private void SetWithoutErrorView()
        {
            this.ViewData["showError"] = "none";
        }

        private void SetAnonimoysView()
        {
            this.ViewData["isAuthenticated"] = "none";
        }
    }
}
