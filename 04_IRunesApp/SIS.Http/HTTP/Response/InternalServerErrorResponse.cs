using System;
using SIS.Http.Common;
using SIS.Http.Enums;

namespace SIS.Http.HTTP.Response
{
    public  class InternalServerErrorResponse:ViewResponse
    {
        public InternalServerErrorResponse(Exception ex,bool fullStackTrace = false)
            :base(HttpStatusCode.InternalServerError,new InternalServerErrorView(ex))
        {
            this.StatusCode = Enums.HttpStatusCode.InternalServerError;
        }
    }
}
