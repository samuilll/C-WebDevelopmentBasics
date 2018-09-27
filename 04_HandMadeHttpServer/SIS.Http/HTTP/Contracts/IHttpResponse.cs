using SIS.Http.Enums;

namespace SIS.Http.HTTP.Contracts
{
  public  interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }

        IHttpHeaderCollection Headers { get; }

        IHttpCookieCollection Cookies { get; }
    }
}   
