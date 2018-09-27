using SIS.ByTheCakeApp.Models;
using SIS.ByTheCakeApp.ViewModels;

namespace SIS.ByTheCakeApp.Services.Contracts
{
   public interface IUserService
    {
        bool Create(string username,string password);

        bool UserExist(string name, string password);

        ProfileViewModel GetUserModelOrNull(string name, string password);
    }
}
