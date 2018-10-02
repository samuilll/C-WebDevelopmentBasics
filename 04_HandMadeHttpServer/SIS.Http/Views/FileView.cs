using SIS.Http.Contracts;
using SIS.Http.HTTP.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SIS.Http.Common;

namespace SIS.Http.Views
{
    public class FileView : IView
    {
        private IDictionary<string, string> viewData = new Dictionary<string, string>();

        private string result;

        public string DefaultPath = GlobalConstants.DefaultPath;

        public const string ContentPlaceholder = "{{{content}}}";

        public FileView(string filename,IDictionary<string,string> data)
        {
            this.viewData = data;

            this.result = this.ProcessFileHtml(filename);

            ReplaceDictionaryItems();
        }

        public FileView(string filename)
        {
            this.result = this.ProcessFileHtml(filename);

            ReplaceDictionaryItems();
        }

        public FileView(string path,string ext)
        {
            CreatePathWithExplicitExtension(path);

            this.result = ProcessNonHtmlFile();

            ReplaceDictionaryItems();
        }

        private void CreatePathWithExplicitExtension(string path)
        {
            var splitPath = this.DefaultPath.Split('/');

            var splitPathWithoutLastElement = splitPath.Take(splitPath.Length - 1).ToArray();

            splitPathWithoutLastElement[splitPathWithoutLastElement.Length - 1] =
              splitPathWithoutLastElement[splitPathWithoutLastElement.Length - 1] + path;

            this.DefaultPath = string.Join("/", splitPathWithoutLastElement);
        }

        private string ProcessNonHtmlFile()
        {
            return File.ReadAllText(this.DefaultPath);
        }

        public string View()
        {
            return this.result;
        }


        private string ProcessFileHtml(string fileName)
        {
            var layout = File.ReadAllText(string.Format(DefaultPath, "layout"));

            var fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));

            var result = layout.Replace(ContentPlaceholder, fileHtml);

            return result;
        }


        private void ReplaceDictionaryItems()
        {
            if (this.viewData.Any())
            {
                foreach (var value in this.viewData)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }
        }
    }
}
