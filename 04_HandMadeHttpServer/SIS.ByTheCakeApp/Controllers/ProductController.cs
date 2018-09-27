using System;
using System.Linq;
using SIS.ByTheCakeApp.Common;
using SIS.ByTheCakeApp.Infrastructure;
using SIS.ByTheCakeApp.Models;
using SIS.ByTheCakeApp.Services;
using SIS.HTTP.HTTP;
using SIS.HTTP.HTTP.Contracts;

namespace SIS.ByTheCakeApp.Controllers
{
    public class ProductController : Controller
    {

        private readonly ProductService productService = new ProductService();

        public IHttpResponse Add()
        {
            this.ViewData["show-result"] = "none";

            return this.FileViewResponse("Product/add");
        }

        public IHttpResponse Add(string name, string priceAsString,string pictureUrl)
        {

            this.ViewData["name"] = name;
            this.ViewData["price"] = priceAsString;
            this.ViewData["url"] = pictureUrl;

           bool success =  this.productService.Add(name, priceAsString, pictureUrl);

            if (success)
            {
                this.ViewData["show-result"] = "block";
            }
            else
            {
                this.InsertErrorMessage(AppConstants.InvalidProduct);
            }

            return this.FileViewResponse("Product/add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            var urlParams = req.UrlParameters;

            this.ViewData["show-cart"] = "none";
            this.ViewData["show-products"] = "none";
            this.ViewData["search-term"] = "Search products...";

            var shoppingCard = req.Session.Get<ShoppingCard>(SessionStore.ShoppingCardKey);

            var productsCount = shoppingCard.Orders.Count;

            if (productsCount > 0)
            {
                var productsText = productsCount == 1 ? "Product" : "Products";

                this.ViewData["show-cart"] = "block";
                this.ViewData["products"] = $"{productsCount} {productsText}";
            }

            if (urlParams.ContainsKey("operation"))
            {
                this.ViewData["search-term"] = urlParams["search-term"];

                return this.Search(urlParams["search-term"]);
            }

            return this.FileViewResponse("Product/search");
        }

        private IHttpResponse Search(string searchTerm)
        {
            var products = this.productService.GetAllBySearchedTerm(searchTerm);

            var resultArgs = products
                .Select(p=> $@"<div>{p.Name} ${p.Price} <a href=""/shopping/add/{p.Id}?search-term=={searchTerm}"">Order</a> </div>")
                .ToList();

            var result = string.Join(Environment.NewLine, resultArgs);

            this.ViewData["show-products"] = "block";
            this.ViewData["products"] = result;

            return this.FileViewResponse("Product/search");
        }
        }

    }

