using HandMadeHttpServer.Server.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP.Contracts
{
   public interface IHttpRequest
    {
        Dictionary<string, string> FormData { get; }

        HttpHeaderCollection HeaderCollection { get; }

        IHttpCookieCollection Cookies { get; }

        Dictionary<string, string> UrlParameters { get; }

        Dictionary<string, string> QueryParameters { get; }

        HttpRequestMethod RequestMethod { get; }

        IHttpSession Session { get; }

        string Path { get; }

        string Url { get; }

        void AddUrlParameter(string key,string value);



    }
}
