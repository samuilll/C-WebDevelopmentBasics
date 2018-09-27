using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SIS.Http.Common;
using SIS.Http.HTTP.Contracts;

namespace SIS.Http.HTTP
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string,HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {

            CoreValidator.ThrowIfNull(cookie, nameof(cookie));


            this.cookies[cookie.Key ]=cookie;
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            return this.cookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            var cookie = this.cookies.FirstOrDefault(h => h.Key == key).Value;

            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            if (!this.cookies.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not presented");
            }

            return cookie;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.cookies.Values.GetEnumerator();
        }
        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return this.cookies.Values.GetEnumerator();
        }

        public void Add(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.Add(new HttpCookie(key, value));

        }
    }
}
