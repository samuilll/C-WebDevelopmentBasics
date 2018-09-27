using SIS.Http.Contracts;

namespace SIS.Http.Common
{
   public class NotFoundView:IView
    {
        public string View()
        {
            return $"<h1>404 This page does not exis!</h1>";
        }
    }
}
