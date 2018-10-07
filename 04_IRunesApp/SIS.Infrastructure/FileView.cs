using System.Collections.Generic;
using System.IO;
using System.Linq;
using SIS.Http.Contracts;

namespace SIS.Infrastructure
{
    public class FileView : IView
    {
        private IDictionary<string, string> viewData;

        private string result;

        public string Path = GlobalConstants.BasePath +"{0}{1}";

        public const string DefaultExtension = ".html";

        public const string ContentPlaceholder = "{{{content}}}";


        public FileView(string filename,IDictionary<string,string> viewData)
        {
            this.viewData = viewData;

            this.result = this.ProcessFile(filename, DefaultExtension);

            ReplaceDictionaryItems();
        }

        public FileView(string filename,string extension)
        {
            this.result = this.ProcessFile(filename,extension);
        }

        public string View()
        {
            return this.result;
        }


        private string ProcessFile(string fileName,string extension)
        {
            string result;

            string fullPath = (string.Format(this.Path, fileName, extension));
            if (extension == DefaultExtension)
            {
                string layout = File.ReadAllText(string.Format(this.Path, "layout", extension));

                string file = File.ReadAllText(fullPath);

                result = layout.Replace(ContentPlaceholder, file);
            }
            else
            {
                result = File.ReadAllText(fullPath);
            }

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
