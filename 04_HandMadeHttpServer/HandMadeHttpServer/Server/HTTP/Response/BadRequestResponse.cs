using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP.Response
{
    using Contracts;
    using HandMadeHttpServer.Server.Enums;

    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse():base()
        {
            this.StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
