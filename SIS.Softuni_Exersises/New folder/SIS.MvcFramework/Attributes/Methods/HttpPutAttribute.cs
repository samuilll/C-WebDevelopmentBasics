﻿namespace SIS.MvcFramework.Attributes.Methods
{
   public class HttpPutAttribute:HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
        {
            if (requestMethod.ToUpper() == "PUT")
            {
                return true;
            }

            return false;
        }
    }
}
