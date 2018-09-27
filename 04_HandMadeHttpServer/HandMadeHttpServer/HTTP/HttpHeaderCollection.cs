using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.HTTP.Contracts;

namespace SIS.HTTP.HTTP
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, ICollection<HttpHeader>> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, ICollection<HttpHeader>>();
        }

        public void Add(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));

            var headerKey = header.Key;

            if (!this.headers.ContainsKey(headerKey))
            {
                this.headers[header.Key] = new List<HttpHeader>();
            }

            this.headers[headerKey].Add(header);
        }

        public void Add(string key, string value)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            CoreValidator.ThrowIfNull(value, nameof(value));

            var header = new HttpHeader(key, value);

            if (!this.headers.ContainsKey(header.Key))
            {
                this.headers[header.Key] = new List<HttpHeader>();
            }

            this.headers[header.Key].Add(header);
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            return this.headers.Any(h => h.Key == key);

        }


        public ICollection<HttpHeader> Get(string key)
        {
            var header = this.headers.FirstOrDefault(h => h.Key == key).Value;

            CoreValidator.ThrowIfNull(header, nameof(header));

            if (!this.headers.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not presented");
            }

            return this.headers[key];
        }
 
        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var header in this.headers)
            {
                foreach (var headerValue in header.Value)
                {
                    result.AppendLine($"{headerValue.Key}: {headerValue.Value}");
                }
            }

            return result.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.headers.Values.GetEnumerator();
        }


        public IEnumerator<ICollection<HttpHeader>> GetEnumerator()
        {
            return this.headers.Values.GetEnumerator();
        }
    }
}
