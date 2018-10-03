using System.Text;
using SIS.Http.Common;
using SIS.Http.Enums;
using SIS.Http.HTTP.Contracts;

namespace SIS.Http.HTTP.Response
{
    public abstract class HttpResponse:IHttpResponse
    {

        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
        }

        public IHttpHeaderCollection Headers { get;protected set; }

        public HttpStatusCode StatusCode { get;protected set; }

        public IHttpCookieCollection Cookies { get; protected set; }

        protected string StatusMessage => this.StatusCode.ToString();

        public override string ToString()
        {
            var response = new StringBuilder();

            response.AppendLine($"{Constants.HttpOneProtocolFragment} {(int)this.StatusCode} {this.StatusMessage}");

            response.AppendLine(this.Headers.ToString());

            return response.ToString();
        }
    }
}
