using System.Collections.Generic;
using SIS.ByTheCakeData.ViewModels;

namespace SIS.ByTheCakeData.Services.Contracts
{
   public interface IUserService
    {
        bool Create(string username,string password);

        bool UserExist(string name, string password);

        ProfileViewModel GetUserModelOrNull(string name, string password);

        ICollection<OrderViewModel> GetOrders(int id);
    }
}
