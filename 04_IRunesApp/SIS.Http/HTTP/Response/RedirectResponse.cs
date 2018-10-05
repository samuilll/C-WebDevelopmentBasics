using SIS.Http.Enums;

namespace SIS.Http.HTTP.Response
{
  public  class RedirectResponse:HttpResponse
    {
        public RedirectResponse(string redirectUrl)
            : base()
        {
            this.StatusCode = HttpStatusCode.Found;

            this.Headers.Add(new HttpHeader(HttpHeader.Location, redirectUrl));
        }
    }
}
