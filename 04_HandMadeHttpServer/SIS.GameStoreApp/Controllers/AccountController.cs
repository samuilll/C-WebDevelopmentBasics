using System;
using System.Collections.Generic;
using System.Text;
using GamesStoreData.Models.ViewModels;
using GamesStoreData.Services;
using GamesStoreData.Services.Contracts;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;
using SIS.Infrastructure;

namespace SIS.GameStoreApp.Controllers
{
  public  class AccountController:BaseController
  {
      protected IUserService userService;

      public AccountController()
      {
            this.userService = new UserService(base.mapper);
      }      

        public IHttpResponse Login(IHttpSession session)
        {
            if (session.IsAuthenticated())
            {
                return new RedirectResponse("/");
            }

          return  this.FileViewResponse("Account/login");
        }

      public IHttpResponse Login(IHttpSession session,Dictionary<string,string> formData)
      {
          string email = formData["email"];
          string password = formData["password"];
      
          LoginViewModel user = this.userService.GetByMailAndPass( email, password);

          if (user!=null)
          {
              session.Add(SessionStore.CurrentUserKey,user);


              return new RedirectResponse("/");             
          }

          return this.FileViewResponse("Account/login");
      }

        public IHttpResponse Register(IHttpSession session)
        {
            if (session.IsAuthenticated())
            {
                return new RedirectResponse("/");
            }
            return this.FileViewResponse("Account/register");
        }

        public IHttpResponse Register(Dictionary<string,string> formData)
        {
            string email = formData["email"];
            string fullName = formData["fullName"];
            string password = formData["password"];
            string confirmedPassword = formData["confirmPassword"];

            RegisterViewModel modelUser = new RegisterViewModel()
            {
                FullName = fullName,
                Password = password,
                ConfirmedPassword = confirmedPassword,
                Email = email
            };

           bool success =  this.userService.CreateUser(modelUser);

            if (success)
            {
                return new RedirectResponse("/login");
            }
            else
            {
                return this.FileViewResponse("Account/register-errors");
            }

        }

      public IHttpResponse Logout(IHttpRequest req)
      {
          req.Session.Clear();

          return new RedirectResponse("/");
      }
  }
}
