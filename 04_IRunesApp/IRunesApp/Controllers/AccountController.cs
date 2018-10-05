using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Domain.ViewModels;
using IRunesApp.Common;
using IRunesServices;
using IRunesServices.Contracts;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;
using SIS.Infrastructure;

namespace IRunesApp.Controllers
{
   public class AccountController:BaseController
   {
       private IUserService userService;

       public AccountController()
       {
           this.userService = new UserService();
       }
        public IHttpResponse Login(IHttpSession session)
        {
            if (session.IsLoggedIn())
            {
                this.InsertErrorMessage(AppConstants.AlreadyLogged);

                this.SetLoggedInView();

                return this.FileViewResponse("Home/index");
            }

            this.SetGuestView();

            return this.FileViewResponse("/Users/login");
        }

        internal IHttpResponse Login(IHttpSession session, Dictionary<string, string> formData)
        {
            string usernameOrEmail = formData["username"];
            string password = formData["password"];

            string username = this.userService.GetByMailOrPass(usernameOrEmail, password);

            if (username==null)
            {
                this.InsertErrorMessage(AppConstants.LogInError);
                return this.FileViewResponse("/Users/login");
            }
            
            session.Add(SessionStore.CurrentUserKey, username);

            return new RedirectResponse("/");
        }

        internal IHttpResponse Register(IHttpSession session, Dictionary<string, string> formData)
        {
            string username = formData["username"];
            string password = formData["password"];
            string confirmedPassword = formData["confirmed-password"];
            string email = formData["email"];

            RegisterViewModel model = new RegisterViewModel()
            {
                Username = username,
                Password = password,
                ConfirmedPassword = confirmedPassword,
                Email = email
            };

            if (!Validation.TryValidate(model))
            {
                this.InsertErrorMessage(AppConstants.InputUserDataError);

                this.SetGuestView();

                return this.FileViewResponse("/Users/register");
            }

            bool success = this.userService.CreateUser(model);

            if (!success)
            {
                this.InsertErrorMessage(AppConstants.UsernameOrEmailAlreadyExist);

                this.SetGuestView();

                return this.FileViewResponse("/Users/register");
            }

           session.Add(SessionStore.CurrentUserKey,model.Username);

           return new RedirectResponse("/");
        }

        public IHttpResponse Register(IHttpSession session)
       {
           this.SetGuestView();

            if (session.IsLoggedIn())
           {
               this.InsertErrorMessage(AppConstants.FirstLogout);
               return this.FileViewResponse("/Home/index");
            }

           return this.FileViewResponse("/Users/register");
       }

       public IHttpResponse Logout(IHttpSession session)
       {
           if (!session.IsLoggedIn())
           {
               return new RedirectResponse("/");
           }

           session.Clear();

           return new RedirectResponse("/");
       }
   }
}
