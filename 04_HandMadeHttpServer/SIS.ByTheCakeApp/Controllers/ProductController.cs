using System;
using System.Linq;
using SIS.ByTheCakeApp.Common;
using SIS.ByTheCakeData.Models;
using SIS.ByTheCakeData.Services;
using SIS.ByTheCakeData.ViewModels;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.Infrastructure;


namespace SIS.ByTheCakeApp.Controllers
{
    using System.Collections.Generic;

    public class ProductController : Controller
    {

        private readonly ProductService productService = new ProductService();

        public ProductController()
        {
        }

        public IHttpResponse Add(IHttpRequest req)
        {
            base.SetUserGreeting(req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey).Name);

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
            base.SetUserGreeting(req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey).Name);

            var urlParams = req.UrlParameters;

            this.ViewData["show-order"] = "none";
            this.ViewData["show-products"] = "none";
            this.ViewData["search-term"] = "Search products...";

            var shoppingCard = req.Session.Get<ShoppingCard>(SessionStore.ShoppingCardKey);

            var productsCount = shoppingCard.ProductIds.Count;

            if (productsCount > 0)
            {
                var productsText = productsCount == 1 ? "Product" : "Products";

                this.ViewData["show-order"] = "block";
                this.ViewData["products-count"] = $"{productsCount.ToString()} {productsText}";
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
                .Select(p=> $@"<div><a href=""/product/details/{p.Id}?search-term={searchTerm}"">{p.Name}</a> ${p.Price} <a href=""/shopping/add/{p.Id}?search-term={searchTerm}"">Order</a> </div>")
                .ToList();

            var result = string.Join(Environment.NewLine, resultArgs);

            this.ViewData["show-products"] = "block";
            this.ViewData["products"] = result;

            return this.FileViewResponse("Product/search");
        }

        public IHttpResponse OrderProductDetails(IHttpRequest req)
        {
            base.SetUserGreeting(req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey).Name);

            Dictionary<string, string> urlParams = req.UrlParameters;

            int productId = int.Parse(urlParams["id"]);

            string orderIdString = urlParams["order-id"];

            ProductViewModel product = this.productService.GetByIdOrNull(productId);

            if (product == null)
            {
                this.ViewData["product-details"] = AppConstants.NoSuchProduct;

                return this.FileViewResponse($"Product/details");
            }

            this.ViewData["order-id"] = orderIdString;
            this.ViewData["product-details"] = product.ToString();

            return this.FileViewResponse($"Product/partOfOrderDetails");
        }

        public IHttpResponse Details(IHttpRequest req)
        {
            base.SetUserGreeting(req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey).Name);

            Dictionary<string,string> urlParams = req.UrlParameters;

            int id = int.Parse(urlParams["id"]);

            ProductViewModel product = this.productService.GetByIdOrNull(id);

            if (urlParams.ContainsKey("search-term"))
            {
                this.ViewData["search-term"] = urlParams["search-term"];
            }

            if (product==null)
            {
                this.ViewData["product-details"] = AppConstants.NoSuchProduct;

                return this.FileViewResponse($"Product/details");
            }

            this.ViewData["product-details"] = product.ToString();

            return this.FileViewResponse($"Product/details");
        }
    }

    }

