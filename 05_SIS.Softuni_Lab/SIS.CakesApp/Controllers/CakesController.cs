using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SIS.CakesApp.Models;
using SIS.CakesApp.ViewModels;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework.Attributes.HttpAttributes;

namespace SIS.CakesApp.Controllers
{
    public class CakesController : BaseController
    {
        [HttpGet("/cakes/add")]
        public IHttpResponse AddCakes()
        {
            return this.View("AddCakes");
        }
        [HttpPost("/cakes/add")]
        public IHttpResponse DoAddCakes(DoAddCakeInputModel model)
        {
            IHttpRequest request = this.Request;

            var name = model.Name;
            var price = model.Price;
            var picture = model.Picture;

            // TODO: Validation

            var product = new Product
            {
                Name = name,
                Price = price,
                ImageUrl = picture
            };
            this.Db.Products.Add(product);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: Log error
                return this.ServerError(e.Message);
            }

            // Redirect
            return this.Redirect("/");
        }
        [HttpGet("/cakes/view")]
        public IHttpResponse ById()
        {
            IHttpRequest request = this.Request;

            var id = int.Parse(request.QueryData["id"].ToString());
            var product = this.Db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return this.BadRequestError("Cake not found.");
            }

            var viewBag = new Dictionary<string, string>
            {
                {"Name", product.Name},
                {"Price", product.Price.ToString(CultureInfo.InvariantCulture)},
                {"ImageUrl", product.ImageUrl}
            };
            return this.View("CakeById", viewBag);
        }
    }
}
