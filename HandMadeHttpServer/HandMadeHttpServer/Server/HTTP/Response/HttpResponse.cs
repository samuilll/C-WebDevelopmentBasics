using HandMadeHttpServer.Server.Contracts;
using HandMadeHttpServer.Server.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP.Response
{
    using Contracts;
    public abstract class HttpResponse:IHttpResponse
    {

        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
        }
        public HttpHeaderCollection Headers { get;protected set; }
        public HttpStatusCode StatusCode { get;protected set; }
        protected string StatusMessage => this.StatusCode.ToString();
        public override string ToString()
        {
            var response = new StringBuilder();

            response.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusMessage}");
            response.AppendLine(this.Headers.ToString());
            response.AppendLine();

            return response.ToString();
        }
    }
}
