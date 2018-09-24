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
        public const string DefaultPath = "ByTheCakeApplication/Resourses/{0}.html";

        public const string ContentPlaceholder = "{{{content}}}";

        public IHttpResponse FileViewResponse(string fileName)
        {
            string result = ProcessFileHtml(fileName);

            return new ViewResponse(HttpStatusCode.OK, new FileView(result));
        }

        public IHttpResponse FileViewResponse(string fileName,Dictionary<string,string> values)
        {
            string result = ProcessFileHtml(fileName);

            if (values != null && values.Any())
            {
                foreach (var value in values)
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
