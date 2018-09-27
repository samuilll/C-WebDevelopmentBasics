using System.Collections.Generic;

namespace SIS.HTTP.HTTP.Contracts
{
    public interface IHttpCookieCollection:IEnumerable<HttpCookie>
    {

        void Add(HttpCookie cookie);

        bool ContainsKey(string key);

        void Add(string key, string value);

        HttpCookie GetCookie(string key);
    }
}
