using System;
using System.Linq;
using SIS.ByTheCakeApp.Models;
using SIS.ByTheCakeApp.Services.Contracts;
using SIS.ByTheCakeApp.ViewModels;

namespace SIS.ByTheCakeApp.Services
{
    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {
            using (var db = new ShoppingDbContext())
            {
                if (db.Users.Any(u => u.Username == username)) return false;

                var user = new User
                {
                    Username = username,
                    Password = password,
                    RegistrationDate = DateTime.UtcNow
                };

                db.Users.Add(user);
                db.SaveChanges();
            }

            return true;
        }

        public bool UserExist(string name, string password)
        {
            using (var db = new ShoppingDbContext())
            {
                bool userExist = db
                                 .Users
                                 .Any(u => u.Username == name && u.Password == password);

                return userExist;
            }
        }

        public ProfileViewModel GetUserModelOrNull(string name, string password)
        {
            using (var db = new ShoppingDbContext())
            {
                var user = db
                    .Users
                    .FirstOrDefault(u => u.Username == name && u.Password == password);

                if (user==null)
                {
                    return null;
                }
                var userViewModel = new ProfileViewModel()
                {
                    Name = user.Username,
                    RegistrationDate = user.RegistrationDate,
                    OrdersCount = user.Orders.Count
                };

                return userViewModel;
            }
        }
    }
}