using HandMadeHttpServer.Server.Contracts;
using HandMadeHttpServer.Server.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP.Response
{
  public  class ViewResponse:HttpResponse
    {
        private readonly IView view;

        public ViewResponse(HttpStatusCode statusCode,IView view)
            : base()
        {
            this.view = view;
            this.StatusCode = statusCode;

            this.Headers.Add(new HttpHeader(HttpHeader.ContentType,"text/html"));
        }

        public override string ToString()
        {
            var result = new StringBuilder(base.ToString());

            var statusCode = (int)this.StatusCode;

            if (statusCode < 300 || statusCode > 399)
            {
                result.AppendLine(this.view.View());
            }
            return result.ToString();
        }
    }
}
