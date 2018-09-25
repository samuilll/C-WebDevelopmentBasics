using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP.Response
{
    using Contracts;
    using Common;
    using HandMadeHttpServer.Server.Enums;

    public  class NotFoundResponse:ViewResponse
    {
        public NotFoundResponse()
            :base(HttpStatusCode.NotFound,new NotFoundView())
        {
            this.StatusCode = Enums.HttpStatusCode.NotFound;
        }
    }
}
