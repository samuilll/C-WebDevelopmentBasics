using System;
using System.Collections.Generic;
using System.Text;

namespace IRunesServices.Contracts
{
   public interface IHashService
   {
       string StrongHash(string password);
   }

  
}
