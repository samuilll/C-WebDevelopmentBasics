using SIS.HTTP.Common;
using SIS.HTTP.Enums;

namespace SIS.HTTP.HTTP.Response
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
