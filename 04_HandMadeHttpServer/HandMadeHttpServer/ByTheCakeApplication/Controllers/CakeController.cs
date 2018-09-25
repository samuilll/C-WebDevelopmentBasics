using HandMadeHttpServer.ByTheCakeApplication.Data;
using HandMadeHttpServer.ByTheCakeApplication.Models;
using HandMadeHttpServer.Infrastructure;
using HandMadeHttpServer.Server.HTTP;
using HandMadeHttpServer.Server.HTTP.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HandMadeHttpServer.ByTheCakeApplication.Controllers.Home
{
    public class CakeController : Controller
    {

        private readonly CakeManager cakeManager = new CakeManager();

        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse("Cake/add");
        }
        public IHttpResponse Add(string name, string priceAsString)
        {
            this.cakeManager.Add(name,priceAsString);

            this.ViewData["name"] = name;
            this.ViewData["price"] = priceAsString;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse("Cake/add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            var urlParams = req.UrlParameters;

            this.ViewData["showCart"] = "none";
            this.ViewData["showCakes"] = "none";
            this.ViewData["searchTerm"] = "Search cakes...";

            var shoppingCard = req.Session.Get<ShoppingCard>(SessionStore.ShoppingCardKey);

            var productsCount = shoppingCard.Orders.Count;

            if (productsCount > 0)
            {
                var productsText = productsCount == 1 ? "Product" : "Products";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{productsCount} {productsText}";
            }

            if (urlParams.ContainsKey("operation"))
            {
                this.ViewData["searchTerm"] = urlParams["searchTerm"];

                return this.Search(urlParams["searchTerm"]);
            }

            return this.FileViewResponse("Cake/search");
        }

        private IHttpResponse Search(string searchTerm)
        {
            var searchedCakes = this.cakeManager.GetAllSearched(searchTerm);

            var resultArgs = searchedCakes
                .Select(c=> $@"<div>{c.Name} ${c.Price} <a href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a> </div>")
                .ToList();

            var result = string.Join(Environment.NewLine, resultArgs);

            this.ViewData["showCakes"] = "block";
            this.ViewData["cakes"] = result;

            return this.FileViewResponse("Cake/search");
        }
        }

    }

