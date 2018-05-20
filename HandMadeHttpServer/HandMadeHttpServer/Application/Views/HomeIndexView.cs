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
            return "<body><h1>Welcome</h1></body>";
        }
    }
}
