using SIS.Http.Enums;

namespace SIS.Http.HTTP.Response
{
    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse()
        {
            this.StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
