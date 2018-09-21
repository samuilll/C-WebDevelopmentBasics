using HandMadeHttpServer.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Application.Views
{
  public  class HomeIndexView:IView
    {

        public string View()
        {
            return "<body><h1>By the cake</h1></body>" +
                   "<body><h2> Enjoy our awesome cakes</h2></body>";
        }
    }
}
