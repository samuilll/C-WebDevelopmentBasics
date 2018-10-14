using SIS.CakesApp.Controllers;
using SIS.MvcFramework;
using SIS.MvcFramework.Logger;
using SIS.MvcFramework.Services;
using SIS.MvcFramework.Services.Contracts;
namespace SIS.CakesApp
{
  public  class StartUp:IMvcApplication
    {
        public void Configure()
        { 
        }

        public void ConfigureServices(IServiceCollection collection)
        {
            collection.AddService<IHashService, HashService>();
            collection.AddService<IUserCookieService, UserCookieService>();
            collection.AddService<ILogger, FileLogger>();
        }
    }
}
