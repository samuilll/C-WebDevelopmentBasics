using System.Linq;
using SIS.ByTheCakeApp.Infrastructure;
using SIS.ByTheCakeApp.Models;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;


namespace SIS.ByTheCakeApp.Controllers
{
    using Services;
    using SIS.ByTheCakeApp.ViewModels;
    using Common;
    using System.Collections.Generic;
    using SIS.ByTheCakeApp.Services.Contracts;
    using System;

    public class ShoppingController:Controller
    {
        private readonly ProductService productService = new ProductService();

        private readonly IShoppingService shoppingService = new ShoppingService();

        public ShoppingController()
        {
        }

        public IHttpResponse AddToOrder(IHttpRequest req)
        {
            base.SetUserGreeting(req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey).Name);

            var productId = int.Parse(req.UrlParameters["id"]);

            bool productExists = this.productService.ExistProduct(productId);//this.cakesManager.FindById(idNumber);

            if (!productExists)
            {
                this.InsertErrorMessage(AppConstants.NoSuchProduct);

                return this.FileViewResponse("Home/index");
            }

            var shoppingCard = req.Session.Get<ShoppingCard>(SessionStore.ShoppingCardKey);

            shoppingCard.Add(productId);

            var redirectUrl = "/search";

            const string searchTermKey = "search-term";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?operation=\"Search\"&{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCurrentOrder(IHttpRequest req)
        {
            base.SetUserGreeting(req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey).Name);

            List<int> productIds = GetProductIds(req);

            ICollection<ProductViewModel> products = this.shoppingService.GetOrderProducts(productIds);

            ICollection<string> allOrdersArgs = products
                .Select(p => $"<div class=\"col-sm-4\">{p.ToString()} <br/></div>")
                .ToList();

            var allOrdersString = string.Join("", allOrdersArgs);

            var totalCost = products.Sum(o => o.Price);

            this.ViewData["search-term"] = req.UrlParameters["search-term"];

            this.ViewData["products"] = allOrdersString;
            this.ViewData["totalCost"] = $"Total Cost: ${totalCost}";

            return this.FileViewResponse("Shopping/showCurrentOrder");
        }

        private static List<int> GetProductIds(IHttpRequest req)
        {
            return req.Session.Get<ShoppingCard>(SessionStore.ShoppingCardKey).ProductIds.ToList();
        }

        public IHttpResponse ShowCompleteOrder(IHttpRequest req)
        {
            base.SetUserGreeting(req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey).Name);

            int orderId = int.Parse(req.UrlParameters["id"]);

            var order = this.shoppingService.GetOrderById(orderId);

            ICollection<ProductViewModel> products = this.productService.GetAllByOrderId(orderId);

            List<string> resultArgs = products
               .Select(p => $@"<tr> <td><a href=""/product/partOfOrderDetails/{p.Id}?order-id={orderId}"">{p.Name}</a></td><td>{p.Price}</td><br/>")
               .ToList();

            string result = string.Join("", resultArgs);

            this.ViewData["date-info"] = $"Created on: { order.CreationDate.ToString("dd-MM-yyyy")}";
            this.ViewData["order-id"] =orderId.ToString() ;
            this.ViewData["products"] = result;

            return this.FileViewResponse("Shopping/showCompleteOrder");
        }

        public IHttpResponse Success(IHttpRequest req)
        {
            base.SetUserGreeting(req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey).Name);

            List<int> productIds = GetProductIds(req);

            ProfileViewModel user = req.Session.Get<ProfileViewModel>(SessionStore.CurrentUserKey);

            int userId = user.Id;

            this.shoppingService.CreateOrder(productIds,userId);

            req.Session.Get<ShoppingCard>(SessionStore.ShoppingCardKey).Clear();

            return this.FileViewResponse("Shopping/success");
        }
    }
}
