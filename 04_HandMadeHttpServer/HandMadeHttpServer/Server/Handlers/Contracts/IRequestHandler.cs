using HandMadeHttpServer.Server.HTTP.Contracts;
using HandMadeHttpServer.Server.HTTP.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Handlers.Contracts
{
    public interface IRequestHandler
    {

        IHttpResponse Handle(IHttpContext httpContext);
    }
}
