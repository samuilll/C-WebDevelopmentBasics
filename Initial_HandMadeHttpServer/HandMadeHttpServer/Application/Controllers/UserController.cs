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
        public IHttpResponse RegisterPost(string name)
        {
            return new RedirectResponse($"/user/{name}");
        }

        public IHttpResponse Details(string name)
        {
            Model model = new Model { ["name"] = name};
            return new ViewResponse(HttpStatusCode.OK, new UserDetailsView(model));
        }
    }
}
