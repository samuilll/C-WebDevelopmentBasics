using SIS.ByTheCakeApp.Infrastructure;
using SIS.Http.HTTP.Contracts;

namespace SIS.ByTheCakeApp.Controllers
{
   public class HomeController:Controller
    {
        public IHttpResponse Index(IHttpRequest req)
        {
            if (!req.Session.IsAuthenticated())
            {
                this.ViewData["show-login"] = "block";
            }

         return   this.FileViewResponse("Home/index");
        }

        public IHttpResponse About() => this.FileViewResponse("Home/about");
                          
    }
}
