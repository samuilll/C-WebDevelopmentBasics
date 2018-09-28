using SIS.ByTheCakeApp.Models;
using SIS.ByTheCakeApp.ViewModels;
using System.Collections.Generic;

namespace SIS.ByTheCakeApp.Services.Contracts
{
   public interface IUserService
    {
        bool Create(string username,string password);

        bool UserExist(string name, string password);

        ProfileViewModel GetUserModelOrNull(string name, string password);

        ICollection<OrderViewModel> GetOrders(int id);
    }
}
