using SIS.Http.HTTP.Contracts;

namespace SIS.Http.HTTP
{
    public class HttpContext : IHttpContext
    {
        private readonly IHttpRequest request;

        public HttpContext(string requestStr)
        {
            this.request = new HttpRequest(requestStr);
        }

        public IHttpRequest Request => this.request;

    }
}
