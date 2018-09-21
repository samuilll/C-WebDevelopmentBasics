using HandMadeHttpServer.Server.HTTP.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Handlers
{
   public class GetHandler:RequestHandler
    {
        public GetHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
            : base(handlingFunc)
        {
        }
    }
}
