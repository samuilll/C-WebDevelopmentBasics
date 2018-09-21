using HandMadeHttpServer.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Contracts
{
   public interface IApplication
    {
        void Start(IAppRouteConfig appRouteConfig);
    }
}
