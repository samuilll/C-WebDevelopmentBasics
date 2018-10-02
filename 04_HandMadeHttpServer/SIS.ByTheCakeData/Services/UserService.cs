using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SIS.ByTheCakeData.Models;
using SIS.ByTheCakeData.Services.Contracts;
using SIS.ByTheCakeData.ViewModels;

namespace SIS.ByTheCakeData.Services
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
                    Id = user.Id,
                    Name = user.Username,
                    RegistrationDate = user.RegistrationDate,
                    OrdersCount = user.Orders.Count
                };

                return userViewModel;
            }
        }

        public ICollection<OrderViewModel> GetOrders(int id)
        {
            using (ShoppingDbContext db = new ShoppingDbContext())
            {
                ICollection<OrderViewModel> orders = db
                    .Users
                    .Include(u=>u.Orders)
                    .ThenInclude(o=>o.Products)
                    .ThenInclude(p=>p.Product)
                    .First(u => u.Id == id)
                    .Orders
                    .Select(o => new OrderViewModel
                    {
                        OrderId = o.Id,
                        CreationDate = o.CreationDate,
                        TotalSum = o.Products.Sum(op => op.Product.Price)
                    })
                    .ToList();

                return orders;
            }
        }
    }
}