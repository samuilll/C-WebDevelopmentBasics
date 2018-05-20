using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP
{
    using Contracts;
    using HandMadeHttpServer.Server.Common;
    using System.Linq;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));

            this.headers[header.Key] = header;
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            return this.headers.Any(h => h.Key == key);

        }

        public HttpHeader GetHeader(string key)
        {
            var header = this.headers.FirstOrDefault(h => h.Key == key).Value;

            CoreValidator.ThrowIfNull(header, nameof(header));

            return header;
        }
 
        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.headers.Select(h=>h.Value.ToString()));
        }
    }
}
