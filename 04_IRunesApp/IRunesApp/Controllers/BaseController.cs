using System.Collections.Generic;
using SIS.Http.Enums;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;
using SIS.Infrastructure;

namespace IRunesApp.Controllers
{
   public abstract class BaseController
    {

        protected IDictionary<string, string> ViewData { get; set; }

        protected BaseController()
        {
            ViewData = new Dictionary<string,string>();

            this.SetWithoutErrorView();
            this.ViewData["show-guest-welcome"] = "none";
        }

        public IHttpResponse FileViewResponse(string fileName)
        {
            return new ViewResponse(HttpStatusCode.OK, new FileView(fileName,this.ViewData));
        }


        protected void InsertErrorMessage(string message)
        {
            this.ViewData["show-error"] = "block";
            this.ViewData["error"] = message;
        }

        protected void SetGuestView()
        {
            this.ViewData["show-guest-welcome"] = "block";
            this.ViewData["guest-welcome"] = "<h1> Welcome to IRunes</h1>";
            this.ViewData["albums-or-login"] = "<a href=\"/Users/login\">Login</a>";
            this.ViewData["register-or-logout"] = "<a href=\"/Users/register\">Register</a>";
            this.ViewData["is-logged-in-view"] =
                "<p><a href = \"/Users/login\" >Login</a> if you have account</p>" +
                "<p><a  href = \"/Users/register\">Register</a> if you don't</p>";
        }

        protected void SetLoggedInView()
        {
            this.ViewData["albums-or-login"] = "<a href=\"/Albums/all\">Albums</a>";
            this.ViewData["register-or-logout"] = "<a href=\"/Users/logout\">Logout</a>";
        }

        protected void SetUserGreeting(string username)
        {
            this.ViewData["user-view"] = "block";
            this.ViewData["show-login"] = "none";
            this.ViewData["username"] = username;
        }


        protected void SetWithoutErrorView()
        {
            this.ViewData["show-error"] = "none";
        }
    }
}
