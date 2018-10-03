using System.Collections.Generic;
using SIS.Http.Enums;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;

namespace SIS.Infrastructure
{
   public abstract class Controller
    {

        protected IDictionary<string, string> ViewData { get; set; }

        protected Controller()
        {
            ViewData = new Dictionary<string,string>();

            this.SetUserGreeting();

            this.ViewData["show-error"] = "none";
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

        protected void SetAnonymousView()
        {
            this.ViewData["show-login"] = "block";
            this.ViewData["user-view"] = "none";
        }

        protected void SetUserGreeting()
        {
            this.ViewData["user-view"] = "block";
            this.ViewData["show-login"] = "none";
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
