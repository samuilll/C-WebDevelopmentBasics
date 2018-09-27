using System.Linq;
using SIS.ByTheCakeApp.Infrastructure;
using SIS.ByTheCakeApp.Models;
using SIS.HTTP.HTTP;
using SIS.HTTP.HTTP.Contracts;
using SIS.HTTP.HTTP.Response;

namespace SIS.ByTheCakeApp.Controllers
{
    using Services;

   public class ShoppingController:Controller
    {
        private readonly ProductService cakesManager = new ProductService();

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var idNumber = int.Parse(req.UrlParameters["id"]);

            Product cake = null;//this.cakesManager.FindById(idNumber);

            if (cake==null)
            {
                return new NotFoundResponse();
            }

            var shoppingCard = req.Session.Get<ShoppingCard>(SessionStore.ShoppingCardKey);

           // shoppingCard.Add(cake);

            var redirectUrl = "/search";

            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?operation=\"Search\"&{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var orders = req.Session.Get<ShoppingCard>(SessionStore.ShoppingCardKey).Orders.ToList();

            var allOrdersArgs = orders
                .Select(c => $"<div>{c.Name} - ${c.Price.ToString()} <br/></div>");

            var allOrdersString = string.Join("", allOrdersArgs);

            var totalCost = orders.Sum(o => o.Price);

            this.ViewData["cakes"] = allOrdersString;
            this.ViewData["totalCost"] = $"Total Cost: ${totalCost}";
     
            return this.FileViewResponse("Shopping/showCart");
        }

        public IHttpResponse Success(IHttpRequest req)
        {
            req.Session.Get<ShoppingCard>(SessionStore.ShoppingCardKey).Clear();

            return this.FileViewResponse("Shopping/success");
        }
    }
}
