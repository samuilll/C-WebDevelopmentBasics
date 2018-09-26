using HandMadeHttpServer.ByTheCakeApplication.Models;
using HandMadeHttpServer.Server.Enums;
using HandMadeHttpServer.Server.HTTP.Contracts;
using HandMadeHttpServer.Server.HTTP.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HandMadeHttpServer.Infrastructure
{
   public abstract class Controller
    {

        protected IDictionary<string, string> ViewData { get; set; }

        protected Controller()
        {
            ViewData = new Dictionary<string,string>();

            this.ViewData["isAuthenticated"] = "block";
        }

        public virtual void SetUserName(string name)
        {
            this.ViewData["username"] = name;
        }

        public const string DefaultPath = "ByTheCakeApplication/Resourses/{0}.html";

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
    }
}
