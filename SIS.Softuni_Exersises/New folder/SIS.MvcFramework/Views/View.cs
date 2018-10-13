using System.IO;
using SIS.MvcFramework.ActionResults.Contracts;

namespace SIS.MvcFramework.Views
{
   public class View:IRenderable
   {
       private readonly string fullyQualifiedTemplateName;

        public View(string fullyQualifiedTemplateName)
        {
            this.fullyQualifiedTemplateName = fullyQualifiedTemplateName;
        }

       private string ReadFile(string fullyQualifiedTemplateName)
       {
           bool exist = File.Exists(fullyQualifiedTemplateName);

           if (!exist)
           {
               throw new FileNotFoundException($"View does not exist at {fullyQualifiedTemplateName}");
           }

           string result = File.ReadAllText(this.fullyQualifiedTemplateName);

           return result;
       }

       public string Render()
       {
           string fullHtml = this.ReadFile(this.fullyQualifiedTemplateName);

           return fullHtml;
       }
    }
}
