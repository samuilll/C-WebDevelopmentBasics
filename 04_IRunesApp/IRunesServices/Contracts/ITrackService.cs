using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Domain.Models;

namespace IRunesServices.Contracts
{
   public interface ITrackService
   {
       bool Create(string albumId,string name, string link, decimal price);
       Track GetById(string trackId);
   }
}
