using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;

namespace SIS.MvcFramework.Attributes.HttpAttributes
{
   public abstract class HttpAttribute:Attribute
    {
        public string Path { get; }

        protected HttpAttribute(string path)
        {
            Path = path;
        }

        public abstract HttpRequestMethod Method { get; }
    }
}
