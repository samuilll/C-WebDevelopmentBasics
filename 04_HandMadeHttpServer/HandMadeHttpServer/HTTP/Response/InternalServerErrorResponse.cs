using System;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;

namespace SIS.HTTP.HTTP.Response
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
