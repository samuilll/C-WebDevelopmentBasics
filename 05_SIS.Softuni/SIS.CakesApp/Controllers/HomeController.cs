using System.Collections.Generic;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;

namespace CakesWebApp.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            return this.View("Index");
        }

        public IHttpResponse HelloUser()
        { 
            return this.View("HelloUser",new Dictionary<string, string>()
            {
                { "Username",$"{this.GetUsername()}"}
            });
        }
    }
}
