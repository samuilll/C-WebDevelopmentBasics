using SIS.HTTP.HTTP.Contracts;

namespace SIS.HTTP.HTTP
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
