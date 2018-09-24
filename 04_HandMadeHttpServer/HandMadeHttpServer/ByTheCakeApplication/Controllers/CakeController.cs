using HandMadeHttpServer.ByTheCakeApplication.Models;
using HandMadeHttpServer.Infrastructure;
using HandMadeHttpServer.Server.HTTP.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HandMadeHttpServer.ByTheCakeApplication.Controllers.Home
{
    public class CakeController : Controller
    {

        private List<Cake> cakes = new List<Cake>();

        public IReadOnlyList<Cake> Cakes { get => (IReadOnlyList<Cake>)cakes; }

        public IHttpResponse Add()
        {
            return this.FileViewResponse("Cake/add",
                new Dictionary<string, string>() {
                    { "showResult" , "none" }
                });
        }

        public IHttpResponse Add(string name, string priceAsString)
        {
            var price = decimal.Parse(priceAsString);

            var cake = new Cake(name, price);

            this.cakes.Add(cake);

            using (var streamWriter = new StreamWriter("ByTheCakeApplication/Data/database.csv", true))
            {
                streamWriter.WriteLine(cake);
            }

            return this.FileViewResponse("Cake/add",
                new Dictionary<string, string>() {
                    { "name", cake.Name},
                    { "price", cake.Price.ToString()},
                    { "showResult" , "block"}
                });
        }

        public IHttpResponse Search(IDictionary<string,string> urlParams)
        {
            if (urlParams.ContainsKey("operation"))
            {
                return this.Search(urlParams["searchTerm"]);
            }

            return this.FileViewResponse("Cake/search");
        }

        private IHttpResponse Search(string searchedName)
        {
            var sb = new StringBuilder();

            using (var streamReader = new StreamReader("ByTheCakeApplication/Data/database.csv"))
            {
                foreach (string line in streamReader.ReadToEnd().Split(Environment.NewLine))
                {
                    var namePricePair = line.Split(',');

                    var currentName = namePricePair[0];

                    if (currentName.Contains(searchedName))
                    {
                        var price = namePricePair[1];

                        sb.AppendLine($"<p>{currentName} ${price}</p        >");
                    }
                }
                var data = sb.ToString().TrimEnd('\n', '\r');

                return this.FileViewResponse("Cake/search",
                    new Dictionary<string, string>()
                    {
                        {
                            "content", data
                        }
                    });
            }
        }

    }
}
