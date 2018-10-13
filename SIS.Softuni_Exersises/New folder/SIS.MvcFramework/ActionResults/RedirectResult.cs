using SIS.MvcFramework.ActionResults.Contracts;

namespace SIS.MvcFramework.ActionResults
{
  public  class RedirectResult:IRedirectable
  {
        public RedirectResult(string redirectUrl)
        {
            RedirectUrl = redirectUrl;
        }

        public string Invoke() => this.RedirectUrl;

      public string RedirectUrl { get; }
    }
}
