using System;
using System.Collections.Generic;
using System.Text;
using GamesStoreData.Models.ViewModels;

namespace GamesStoreData.Services.Contracts
{
    public interface  IUserService
    {
        bool CreateUser(RegisterViewModel registerModel);
        LoginViewModel GetByMailAndPass(string email, string password);
    }
}
