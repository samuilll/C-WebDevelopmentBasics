using SIS.HTTP.HTTP.Contracts;

namespace SIS.WebServer.Handlers.Contracts
{
    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext httpContext);
    }
}
