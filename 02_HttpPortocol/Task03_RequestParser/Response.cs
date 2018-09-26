using System;
using System.Collections.Generic;
using System.Text;

namespace Task03_RequestParser
{
   internal class Response
    {
        private const string CONTENT_TYPE_VALUE = "text/plain";
        private const string CONTENT_TYPE = "Content-Type";
        private const string CONTENT_LENGTH = "Content-Length";


        private string protocol;
        private int statusCode;
        private string statusText;
        private int contentLength;
        private string contentType;

        public Response(string protocol, int statusCode, string statusText)
        {
            this.protocol = protocol;
            this.statusCode = statusCode;
            this.statusText = statusText;
            this.contentLength = this.statusText.ToString().Length;
            this.contentType = CONTENT_TYPE_VALUE;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.protocol} {this.statusCode} {this.statusText}");
            sb.AppendLine($"{CONTENT_LENGTH}: {this.contentLength}");
            sb.AppendLine($"{CONTENT_TYPE}: {this.contentType}");
            sb.AppendLine();
            sb.AppendLine(this.statusText.ToString());

            return sb.ToString();
        }
    }
}
