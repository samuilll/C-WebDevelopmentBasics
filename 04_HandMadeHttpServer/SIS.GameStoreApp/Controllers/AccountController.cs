using System;
using System.Collections.Generic;
using System.Text;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;
using SIS.Infrastructure;

namespace SIS.GameStoreApp.Controllers
{
  public  class AccountController:Controller
    {
        public IHttpResponse Login()
        {
          return  this.FileViewResponse("Account/login");
        }

        public IHttpResponse Register()
        {
            return this.FileViewResponse("Account/register");
        }
    }
}
