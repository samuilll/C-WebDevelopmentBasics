using SIS.MvcFramework.ActionResults.Contracts;

namespace SIS.MvcFramework.ActionResults
{
   public class ViewResult:IViewable
   {
        public ViewResult(IRenderable view)
        {
            View = view;
        }

        public string Invoke() =>this.View.Render();

        public IRenderable View { get; set; }
    }
}
