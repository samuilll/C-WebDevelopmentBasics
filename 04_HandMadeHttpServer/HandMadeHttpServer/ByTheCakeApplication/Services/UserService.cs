using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.ByTheCakeApplication.Services
{
    using Contracts;
    using Models;
    using System.Linq;

    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {
            using (var db = new ShoppingDbContext())
            {
                if (db.Users.Any(u=>u.Username==username))
                {
                    return false;
                }
                var user = new User()
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
    }
}
