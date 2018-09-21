using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Handlers
{
    using Contracts;
    using HandMadeHttpServer.Server.Common;
    using HandMadeHttpServer.Server.Enums;
    using HandMadeHttpServer.Server.HTTP;
    using HandMadeHttpServer.Server.HTTP.Contracts;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc,nameof(handlingFunc));

            this.handlingFunc = handlingFunc;
        }
        public IHttpResponse Handle(IHttpContext httpContext)
        {
            var response = this.handlingFunc(httpContext.Request);

           // response.Headers.Add(new HttpHeader("Content-Type","text/html"));
            if (!response.Headers.ContainsKey("Content-Type"))
            {
                response.Headers.Add(new HttpHeader("Content-Type", "text/html"));
            }

            return response;
        }       
    }
}
