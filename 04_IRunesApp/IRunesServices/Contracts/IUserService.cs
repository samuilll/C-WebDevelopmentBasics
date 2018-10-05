using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Domain.ViewModels;

namespace IRunesServices.Contracts
{
   public interface IUserService
    {
        bool CreateUser(RegisterViewModel registerModel);

        string GetByMailOrPass(string emailOrUsername, string password);
    }
}
