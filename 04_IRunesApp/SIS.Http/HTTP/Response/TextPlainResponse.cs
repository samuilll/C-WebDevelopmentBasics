using SIS.Http.Contracts;
using SIS.Http.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Http.HTTP.Response
{
 public   class TextPlainResponse:HttpResponse
    {
        private readonly IView view;

        public TextPlainResponse(HttpStatusCode statusCode, IView view)
            : base()
        {
            this.view = view;

            this.StatusCode = statusCode;

        }

        public override string ToString()
        {           
            return $"{base.ToString()} {this.view.View()}";
        }

        public override string ToBaseString()
        {
            return base.ToString();
        }
    }
}
