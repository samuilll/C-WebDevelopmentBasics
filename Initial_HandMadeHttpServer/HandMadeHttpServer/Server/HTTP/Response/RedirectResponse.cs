using HandMadeHttpServer.Server.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP.Response
{
  public  class RedirectResponse:HttpResponse
    {
        public RedirectResponse(string redirectUrl)
            : base()
        {
            this.StatusCode = HttpStatusCode.Found;

            this.Headers.Add(new HttpHeader("Location", redirectUrl));
        }
    }
}
