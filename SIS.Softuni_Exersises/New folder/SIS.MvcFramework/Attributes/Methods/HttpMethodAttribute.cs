using System;

namespace SIS.MvcFramework.Attributes.Methods
{
  public abstract class HttpMethodAttribute:Attribute
  {
      public abstract bool IsValid(string requestMethod);
  }
}
