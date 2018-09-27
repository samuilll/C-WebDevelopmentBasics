using SIS.WebServer.Routing.Contracts;

namespace SIS.WebServer.Contracts
{
   public interface IApplication
    {
        void Configure(IAppRouteConfig appRouteConfig);
    }
}
