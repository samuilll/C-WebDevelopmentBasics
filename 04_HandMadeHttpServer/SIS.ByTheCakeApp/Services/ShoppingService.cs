using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.ByTheCakeApp.Services
{
    using Contracts;
    using SIS.ByTheCakeApp.ViewModels;
    using System.Linq;
    using Models;

    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(ICollection<int> productIds,int userId)
        {
            using (ShoppingDbContext db = new ShoppingDbContext())
            {
                var order = new Order
                {
                    UserId = userId,
                    CreationDate = DateTime.UtcNow,
                    Products = productIds
                        .Select(id => new OrderProduct
                        {
                            ProductId = id
                        })
                        .ToList()
                };

                db.Orders.Add(order);

                db.SaveChanges();
            }
        }

        public OrderViewModel GetOrderById(int orderId)
        {
            using (ShoppingDbContext db = new ShoppingDbContext())
            {
                Order order =  db
                     .Orders
                     .FirstOrDefault(o => o.Id == orderId);

                OrderViewModel orderViewModel = new OrderViewModel()
                {
                    OrderId = order.Id,
                    CreationDate = order.CreationDate
                };

                return orderViewModel;
            }
        }

        public ICollection<ProductViewModel> GetOrderProducts(ICollection<int> productIds)
        {
            using (ShoppingDbContext db = new ShoppingDbContext())
            {
                List<ProductViewModel> products = db
                    .Products
                    .Where(p => productIds.Contains(p.Id))
                    .Select(p => new ProductViewModel(p.Id, p.Name, p.Price, p.ImageUrl))
                    .ToList();

                return products;
            }
        }
    }
}
