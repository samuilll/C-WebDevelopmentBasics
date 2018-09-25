using HandMadeHttpServer.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Common
{
   public class NotFoundView:IView
    {
        public string View()
        {
            return $"<h1>404 This page does not exis!</h1>";
        }
    }
}
