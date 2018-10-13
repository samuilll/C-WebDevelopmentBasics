using SIS.CakesApp.Data;
using SIS.MvcFramework;

namespace SIS.CakesApp.Controllers
{
    public abstract class BaseController:Controller
    {
        protected BaseController()
        {
            this.Db = new CakesDbContext();
        }

        protected CakesDbContext Db { get; }
       
    }
}
