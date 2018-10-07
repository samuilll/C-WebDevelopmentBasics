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

        public const string ViewsFolder = "Views/";

        public const string ResoursesFolder = "Resourses/";

        public string Path = GlobalConstants.BasePath +"{0}{1}{2}";

        public const string DefaultExtension = ".html";

        public const string ContentPlaceholder = "{{{content}}}";


        public FileView(string filename,IDictionary<string,string> viewData)
        {
            this.viewData = viewData;

            this.result = this.ProcessFile(filename,ViewsFolder, DefaultExtension);

            ReplaceDictionaryItems();
        }

        public FileView(string filename,string extension)
        {
            this.result = this.ProcessFile(filename,ResoursesFolder,extension);
        }

        public string View()
        {
            return this.result;
        }


        private string ProcessFile(string fileName,string folder,string extension)
        {
            string result;

            string fullPath = (string.Format(this.Path,folder, fileName,extension));

            if (extension == DefaultExtension)
            {
                string layout = File.ReadAllText(string.Format(this.Path,folder, "layout", extension));

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
