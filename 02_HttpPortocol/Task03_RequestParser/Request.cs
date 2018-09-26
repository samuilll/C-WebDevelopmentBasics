using System;
using System.Collections.Generic;
using System.Text;
using Task03_RequestParser.Enums;

namespace Task03_RequestParser
{
  internal  class Request:IComparable<Request>
    {
        public Request(RequestType type, string path)
        {
            Type = type;
            Path = path;
        }

        public RequestType Type { get;}

        public string Path { get; }

        public int CompareTo(Request other)
        {
            return (string.Equals(this.Type.ToString(), other.Type.ToString())&&string.Equals(this.Path,other.Path)) ? 0 : -1;
        }
    }
}
