using System.Collections.Generic;
using SIS.Http.Common;
using SIS.Http.HTTP.Contracts;

namespace SIS.Http.HTTP
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

        public void Add(string key, object value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value.ToString(), nameof(value));

            this.values[key] = value;

        }

        public void Clear()
        {
            this.values.Clear();
        }

        public bool Contains(string key)
        {
            return this.values.ContainsKey(key);
        }

        public object Get(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (!this.values.ContainsKey(key))
            {
                return null;
            }

            return this.values[key];
        }

        public T Get<T>(string key)
        => (T)this.Get(key);

        public bool IsAuthenticated()
        {
            return this.Contains(SessionStore.CurrentUserKey);
        }
    }
}
