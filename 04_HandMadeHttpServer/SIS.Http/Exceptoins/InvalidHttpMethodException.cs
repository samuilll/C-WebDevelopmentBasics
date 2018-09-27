using System;

namespace SIS.Http.Exceptoins
{
   public class InvalidHttpMethodException:Exception
    {
        public InvalidHttpMethodException()
            : base("Http method is not correct")
        {

        }
    }
}
