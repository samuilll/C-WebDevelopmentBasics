using System;
using System.Collections.Generic;
using System.Text;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;

namespace IRunesApp.Controllers
{
   public class HomeController:BaseController
    {
        public IHttpResponse Index(IHttpRequest req)
        {
            IHttpSession session = req.Session;

            if (session.IsLoggedIn())
            {
                string username = session.Get<string>(SessionStore.CurrentUserKey);

               this.SetLoggedInView();

               this.ViewData["is-logged-in-view"] = $"<h1>Welcome, {username}</h1><h3>IRunes wishes you a fun experience</h3>";
            }
            else
            {
              this.SetGuestView();
            }

            return this.FileViewResponse("Home/index");
        }
    }
}
