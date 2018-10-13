using System.Runtime.CompilerServices;
using SIS.HTTP.Requests;
using SIS.MvcFramework.ActionResults;
using SIS.MvcFramework.ActionResults.Contracts;
using SIS.MvcFramework.Utilities;
using SIS.MvcFramework.Views;

namespace SIS.MvcFramework.Controllers
{
    public abstract class Controller
    {
        public IHttpRequest Request { get; set; }

        protected IViewable View([CallerMemberName] string viewName = "")
        {
            var controllerName = ControllerUtilities.GetControllerName(this);

            var viewFullyQualifiedName = ControllerUtilities
                .GetViewFullyQualifiedName(controllerName, viewName);

            var view = new View(viewFullyQualifiedName);

            return new ViewResult(view);
        }

        protected IRedirectable RedirectToAction(string redirectUrl)
            => new RedirectResult(redirectUrl);
    }
}