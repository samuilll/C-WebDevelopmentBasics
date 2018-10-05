using SIS.Http.Common;
using SIS.Http.Enums;

namespace SIS.Http.HTTP.Response
{
    public  class NotFoundResponse:ViewResponse
    {
        public NotFoundResponse()
            :base(HttpStatusCode.NotFound,new NotFoundView())
        {
            this.StatusCode = Enums.HttpStatusCode.NotFound;
        }
    }
}
