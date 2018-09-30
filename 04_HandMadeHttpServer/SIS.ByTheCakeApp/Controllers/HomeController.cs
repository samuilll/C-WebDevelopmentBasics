using SIS.ByTheCakeApp.Infrastructure;
using SIS.ByTheCakeApp.ViewModels;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;

namespace SIS.ByTheCakeApp.Controllers
{
   public class HomeController:Controller
    {
        public IHttpResponse Index(IHttpRequest req)
        {
            if (!req.Session.IsAuthenticated())
            {
                base.SetAnonymousView();             
            }
            else
            {
                base.SetUserGreeting(req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey).Name);
            }

            return   this.FileViewResponse("Home/index");
        }

        public IHttpResponse About(IHttpRequest req)
        {
            base.SetUserGreeting(req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey).Name);

           return this.FileViewResponse("Home/about");
        }
                          
    }
}
