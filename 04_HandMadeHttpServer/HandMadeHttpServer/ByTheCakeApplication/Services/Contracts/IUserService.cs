using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.ByTheCakeApplication.Services.Contracts
{
   public interface IUserService
    {
        bool Create(string username,string password);
    }
}
