using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP.Response
{
    using Contracts;
    using HandMadeHttpServer.Server.Common;
    using HandMadeHttpServer.Server.Enums;

    public  class InternalServerErrorResponse:ViewResponse
    {
        public InternalServerErrorResponse(Exception ex,bool fullStackTrace = false)
            :base(HttpStatusCode.InternalServerError,new InternalServerErrorView(ex))
        {
            this.StatusCode = Enums.HttpStatusCode.InternalServerError;
        }
    }
}
