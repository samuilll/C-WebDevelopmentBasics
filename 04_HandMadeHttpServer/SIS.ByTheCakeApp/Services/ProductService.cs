using SIS.ByTheCakeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.ByTheCakeApp.Services
{
    using Common;
    using Contracts;
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
    }
}
