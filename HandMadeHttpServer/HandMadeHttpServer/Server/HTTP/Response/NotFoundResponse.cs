using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP.Response
{
    using Contracts;
  public  class NotFoundResponse:HttpResponse
    {
        public NotFoundResponse():base()
        {
            this.StatusCode = Enums.HttpStatusCode.NotFound;
        }
    }
}
