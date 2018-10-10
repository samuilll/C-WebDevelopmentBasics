using SIS.Http.Enums;
using SIS.Http.HTTP.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SIS.Http.HTTP.Response
{
    public class FileResponse : HttpResponse
    {
        public string Content { get; set; }

        public FileResponse(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                this.Content = reader.ReadToEnd();
            }
        }
    }
}
