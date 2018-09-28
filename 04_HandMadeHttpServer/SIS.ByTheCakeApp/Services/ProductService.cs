using SIS.ByTheCakeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.ByTheCakeApp.Services
{
    using Common;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using SIS.ByTheCakeApp.ViewModels;
    using System.Linq;

    public class ProductService:IProductService
    {
        public bool Add(string name, string priceAsString, string pictureUrl)
        {
            var product = new Product()
            {
                Name = name,
                Price = decimal.Parse(priceAsString),
                ImageUrl = pictureUrl
            };

          bool success =   Validation.Validate(product);

            if (success)
            {
                using (var db = new ShoppingDbContext())
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool ExistProduct(int id)
        {
            using (ShoppingDbContext db = new ShoppingDbContext())
            {
                return db
                    .Products
                    .Any(p => p.Id == id);
            }
        }

        public ICollection<ProductViewModel> GetAllByOrderId(int orderId)
        {
            using (ShoppingDbContext db = new ShoppingDbContext())
            {
              return  db
               .Orders
               .Include(o => o.Products)
               .ThenInclude(op => op.Product)
               .Where(o => o.Id == orderId)
               .First()
               .Products
               .Select(op => new ProductViewModel(op.Product.Id, op.Product.Name, op.Product.Price, op.Product.ImageUrl))
               .ToList();
                    
            }
        }

        public ICollection<ProductViewModel> GetAllBySearchedTerm(string searchTerm)
        {
            using (var db = new ShoppingDbContext())
            {

                var allCakes = db
                    .Products
                    .Where(p=>p.Name.Contains(searchTerm))
                    .Select(p => new ProductViewModel(p.Id,p.Name, p.Price, p.ImageUrl))
                    .ToList();

                return allCakes;
            }
        }

        public ProductViewModel GetByIdOrNull(int id)
        {
            using (var db = new ShoppingDbContext())
            {
                ProductViewModel product =  db
                    .Products
                    .Where(p => p.Id == id)
                    .Select(p => new ProductViewModel(p.Id, p.Name, p.Price, p.ImageUrl))
                    .FirstOrDefault();

                return product;
            }
        }
    }
}
