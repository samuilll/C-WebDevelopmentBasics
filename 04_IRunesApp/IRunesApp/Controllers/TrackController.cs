using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Domain.Models;
using IRunesServices;
using IRunesServices.Contracts;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;

namespace IRunesApp.Controllers
{
  public  class TrackController:BaseController
  {
      private ITrackService trackService;

      public TrackController()
      {
          this.trackService = new TrackService();
      }
        public IHttpResponse Create(Dictionary<string, string> urlParameters)
        {
            this.ViewData["albumId"] = urlParameters["albumId"];

            this.SetLoggedInView();


            return this.FileViewResponse("Tracks/create");
        }

        internal IHttpResponse Create(IHttpRequest req)
        {
            string name = req.FormData["name"];
            string link = req.FormData["link"];
            decimal price = decimal.Parse(req.FormData["price"]);

            string albumId = req.UrlParameters["albumId"];

            bool success = this.trackService.Create(albumId, name, link, price);

            if (!success)
            {
                //Can insert error message here
            }

            return new RedirectResponse($"/Albums/details?albumId={albumId}");
        }

        public IHttpResponse Details(IHttpRequest req)
        {
            string trackId = req.UrlParameters["trackId"];
            Track track = this.trackService.GetById(trackId);
            this.ViewData["name"] = track.Name;
            this.ViewData["link"] = track.Link;
            this.ViewData["price"] = track.Price.ToString("f2");

            this.ViewData["albumId"] = req.UrlParameters["albumId"];

            this.SetLoggedInView();

            return this.FileViewResponse("/Tracks/details");
        }
    }
}
