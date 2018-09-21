using HandMadeHttpServer.Application.Views;
using HandMadeHttpServer.Server;
using HandMadeHttpServer.Server.Enums;
using HandMadeHttpServer.Server.HTTP.Contracts;
using HandMadeHttpServer.Server.HTTP.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Application.Controllers
{
   public class UserController
    {

        public IHttpResponse RegisterGet()
        {
            return new ViewResponse(HttpStatusCode.OK,new RegisterView());
        }
        public IHttpResponse RegisterPost(string firstName, string middleName, string lastName)
        {
            var response = new RedirectResponse($"/user?first-name={firstName}&middle-name={middleName}&last-name={lastName}");
            
            response.Cookies.Add($"{firstName}", $"{firstName}{middleName}{lastName}");

            return response;
        }

        public IHttpResponse Details(string fisrtName, string middleName, string lastName)
        {
            Model model = new Model { ["firstName"] = fisrtName,["middleName"]=middleName,["lastName"]=lastName};

            return new ViewResponse(HttpStatusCode.OK, new UserDetailsView(model));
        }
    }
}
