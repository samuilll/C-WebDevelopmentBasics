using HandMadeHttpServer.Server.Common;
using HandMadeHttpServer.Server.HTTP.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP
{
    public class HttpSession : IHttpSession
    {
        private readonly IDictionary<string, object> values;

        public HttpSession(string id)
        {
            CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));

            this.Id = id;
            this.values = new Dictionary<string, object>();
        }

        public string Id { get; private set; }

        public void Add(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.values[key] = value;

        }

        public void Clear()
        {
            this.values.Clear();
        }

        public object Get(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (!this.values.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not presented");
            }

            return this.values[key];
        }

        public bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }
    }
}
