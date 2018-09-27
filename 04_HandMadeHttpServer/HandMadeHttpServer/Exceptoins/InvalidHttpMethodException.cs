using System;

namespace SIS.HTTP.Exceptoins
{
   public class InvalidHttpMethodException:Exception
    {
        public InvalidHttpMethodException()
            : base("Http method is not correct")
        {

        }
    }
}
