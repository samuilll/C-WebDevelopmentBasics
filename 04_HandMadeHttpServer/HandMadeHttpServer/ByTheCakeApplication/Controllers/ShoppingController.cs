using HandMadeHttpServer.ByTheCakeApplication.Data;
using HandMadeHttpServer.ByTheCakeApplication.Models;
using HandMadeHttpServer.Server.HTTP;
using HandMadeHttpServer.Server.HTTP.Contracts;
using HandMadeHttpServer.Server.HTTP.Response;

namespace HandMadeHttpServer.ByTheCakeApplication.Controllers
{
   public class ShoppingController
    {
        private readonly CakeManager cakesManager = new CakeManager();

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var idNumber = int.Parse(req.UrlParameters["id"]);

            var cake = this.cakesManager.FindById(idNumber);

            if (cake==null)
            {
                return new NotFoundResponse();
            }

            var shoppingCard = req.Session.Get<ShoppingCard>(SessionStore.ShoppingCardKey);

            shoppingCard.Orders.Add(cake);

            var redirectUrl = "/search";

            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?operation=\"Search\"&{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }
            return new RedirectResponse(redirectUrl);

        }

        //public IHttpResponse ShowCart(IHttpRequest req)
        //{

        //}
    }
}
