using HandMadeHttpServer.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Common
{
    public class InternalServerErrorView : IView
    {
        private readonly Exception exception;
        public InternalServerErrorView(Exception ex)
        {
            this.exception = ex;
        }
        public string View()
        {
            return $"<h1>{this.exception.Message}</h1><h2>{this.exception.StackTrace}</h2>";

        }
    }
}
