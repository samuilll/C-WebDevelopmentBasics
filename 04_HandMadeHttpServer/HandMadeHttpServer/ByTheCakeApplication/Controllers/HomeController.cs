using HandMadeHttpServer.Server.Enums;
using HandMadeHttpServer.Server.HTTP.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using  HandMadeHttpServer.Server.HTTP.Response;
using System.IO;
using HandMadeHttpServer.Infrastructure;

namespace HandMadeHttpServer.ByTheCakeApplication.Controllers.Home
{
   public class HomeController:Controller
    {
        public IHttpResponse Index() => this.FileViewResponse("Home/index");

        public IHttpResponse About() => this.FileViewResponse("Home/about");
                          
    }
}
