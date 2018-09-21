using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP.Contracts
{
    public interface IHttpCookieCollection:IEnumerable<HttpCookie>
    {

        void Add(HttpCookie cookie);

        bool ContainsKey(string key);

        void Add(string key, string value);

        HttpCookie GetHeader(string key);
    }
}
