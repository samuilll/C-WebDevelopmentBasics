using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.HTTP.Contracts;

namespace SIS.HTTP.HTTP.Response
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

            response.AppendLine($"{GlobalConstants.HttpOneProtocolFragment} {(int)this.StatusCode} {this.StatusMessage}");

            response.AppendLine(this.Headers.ToString());

            return response.ToString();
        }
    }
}
