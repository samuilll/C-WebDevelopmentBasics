namespace SIS.HTTP.HTTP.Contracts
{
    public interface IHttpSession
    {
        string Id { get; }

        object Get(string key);

        void Add(string key, object value);

        void Clear();

        bool Contains(string key);

        bool IsAuthenticated();

        T Get<T>(string key);
    }
}
