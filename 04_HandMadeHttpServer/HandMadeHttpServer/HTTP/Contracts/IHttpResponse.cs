using SIS.HTTP.Enums;

namespace SIS.HTTP.HTTP.Contracts
{
  public  interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }

        IHttpHeaderCollection Headers { get; }

        IHttpCookieCollection Cookies { get; }
    }
}   
