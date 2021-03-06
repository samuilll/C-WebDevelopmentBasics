﻿namespace SIS.MvcFramework.Attributes.Methods
{
   public class HttpDeleteAttribute:HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
        {
            if (requestMethod.ToUpper() == "DELETE")
            {
                return true;
            }

            return false;
        }
    }
}
