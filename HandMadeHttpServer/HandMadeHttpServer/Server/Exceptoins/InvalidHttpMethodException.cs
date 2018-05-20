using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Exceptoins
{
   public class InvalidHttpMethodException:Exception
    {
        public InvalidHttpMethodException()
            : base("Http method is not correct")
        {

        }
    }
}
