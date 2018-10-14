using System.Collections.Generic;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes.HttpAttributes;

namespace SIS.CakesApp.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet("/")]
        public IHttpResponse Index()
        {
            return this.View("Index");
        }
        [HttpGet("/hello")]
        public IHttpResponse HelloUser()
        { 
            return this.View("HelloUser",new Dictionary<string, string>()
            {
                { "Username",$"{this.User}"}
            });
        }
    }
}
