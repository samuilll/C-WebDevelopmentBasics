using System.Collections.Generic;
using System.IO;
using System.Linq;
using SIS.Http.Enums;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;


namespace SIS.ByTheCakeApp.Infrastructure
{
   public abstract class Controller
    {

        protected IDictionary<string, string> ViewData { get; set; }

        protected Controller()
        {
            ViewData = new Dictionary<string,string>();

            this.ViewData["isAuthenticated"] = "block";

            this.ViewData["show-login"] = "none";

            this.ViewData["show-error"] = "none";
        }

        public const string DefaultPath = "../../../Resourses/{0}.html";

        public const string ContentPlaceholder = "{{{content}}}";

        public IHttpResponse FileViewResponse(string fileName)
        {
            string result = ProcessFileHtml(fileName);

            if (this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpStatusCode.OK, new FileView(result));
        }

        private static string ProcessFileHtml(string fileName)
        {
            var layout = File.ReadAllText(string.Format(DefaultPath, "layout"));

            var fileHtml = File.ReadAllText(string.Format(DefaultPath, $"{fileName}"));

            var result = layout.Replace(ContentPlaceholder, fileHtml);
            return result;
        }

        protected void InsertErrorMessage(string message)
        {
            this.ViewData["show-error"] = "block";
            this.ViewData["error"] = message;
        }
    }
}
