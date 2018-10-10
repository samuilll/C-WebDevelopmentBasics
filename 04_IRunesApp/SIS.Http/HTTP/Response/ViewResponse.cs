using SIS.Http.Contracts;
using SIS.Http.Enums;
using System.Text;

namespace SIS.Http.HTTP.Response
{
  public  class ViewResponse:HttpResponse
    {
        private readonly IView view;

        public ViewResponse(HttpStatusCode statusCode,IView view)
            : base()
        {
            this.view = view;

            this.StatusCode = statusCode;

            this.Headers.Add(new HttpHeader(HttpHeader.ContentType,"text/html"));
        }

        public override string ToString()
        {
            var result = new StringBuilder(base.ToString());

            var statusCode = (int)this.StatusCode;

            if (statusCode < 300 || statusCode > 399)
            {
                result.AppendLine(this.view.View());
            }
            return result.ToString();
        }

        public override string ToBaseString()
        {
            return base.ToString();
        }
    }
}
