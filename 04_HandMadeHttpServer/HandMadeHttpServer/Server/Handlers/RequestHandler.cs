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

    public  class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc,nameof(handlingFunc));

            this.handlingFunc = handlingFunc;
        }
        public IHttpResponse Handle(IHttpContext httpContext)
        {
            var response = this.handlingFunc(httpContext.Request);

           // response.Headers.Add(new HttpHeader("Content-Type","text/html"));
            if (!response.Headers.ContainsKey(HttpHeader.ContentType))
            {
                response.Headers.Add(new HttpHeader("Content-Type", "text/html"));
            }

            foreach (var cookie in response.Cookies)
            {
                response.Headers.Add(new HttpHeader(HttpHeader.SetCookie, cookie.ToString()));
            }

            return response;
        }       
    }
}
