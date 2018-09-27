using Microsoft.Extensions.Primitives;
using SIS.ByTheCakeApp.Common;
using SIS.ByTheCakeApp.Infrastructure;
using SIS.ByTheCakeApp.Models;
using SIS.ByTheCakeApp.Services;
using SIS.ByTheCakeApp.Services.Contracts;
using SIS.ByTheCakeApp.ViewModels;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;

namespace SIS.ByTheCakeApp.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;

        public AccountController()
        {
            this.userService = new UserService();
        }

        public IHttpResponse Login(IHttpRequest req)
        {
            if (req.Session.IsAuthenticated())
            {
                InsertErrorMessage(AppConstants.LogOutFirst);

                return this.FileViewResponse("Home/index");
            }

            SetAnonymousView();
            SetWithoutErrorView();

            return this.FileViewResponse("Account/login");
        }

        public IHttpResponse Login(IHttpRequest req, LoginUserViewModel model)
        {
            string name = model.Username;
            string password = model.Password;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                InsertErrorMessage(AppConstants.EmptyUsernameOrPasswordField);

                return this.FileViewResponse("Account/login");
            }

            var userViewModel = this.userService.GetUserModelOrNull(name, password);

            if (userViewModel==null)
            {
                InsertErrorMessage(AppConstants.IncorrectUsernameOrPassword);

                return this.FileViewResponse("Account/login");
            }

            this.LogInUser(req, userViewModel);

            return new RedirectResponse("/");
        }

        internal IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }

        internal IHttpResponse Register()
        {
            SetAnonymousView();
            SetWithoutErrorView();

            return this.FileViewResponse("Account/register");
        }

        internal IHttpResponse Register(RegisterUserViewModel model)
        {
            var username = model.Username;
            var password = model.Password;
            var confirmPassword = model.ConfirmPassword;

            if (username.Length < 3 || password.Length < 3 || confirmPassword != password)
            {
                InsertErrorMessage(AppConstants.InvalidUserParameters);

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
                this.ViewData["show-error"] = "block";
                this.ViewData["error"] = "This username is taken";

                return this.FileViewResponse("Account/register");
            }
        }

        private void SetWithoutErrorView()
        {
            this.ViewData["show-error"] = "none";
        }

        private void SetAnonymousView()
        {
            this.ViewData["isAuthenticated"] = "none";
        }

        private void LogInUser(IHttpRequest req, ProfileViewModel user)
        {
            req.Session.Add(SessionStore.CurrentUserKey, user);
            req.Session.Add(SessionStore.ShoppingCardKey, new ShoppingCard());
        }

        public IHttpResponse ProfileView(IHttpRequest req)
        {
            if (!req.Session.IsAuthenticated())
            {
                InsertErrorMessage(AppConstants.NoLoggedUser);

                return this.FileViewResponse("Account/login");
            }

            var userViewModel = req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey);

            this.ViewData["profile-view"] = userViewModel.ToString();

            return this.FileViewResponse("Account/profile");
        }
    }
}