namespace SIS.WebServer.Api.Contracts
{
    public interface IHttpHandler
    {
        IHttpResponse Handle(IHttpRequest request);
    }
}
