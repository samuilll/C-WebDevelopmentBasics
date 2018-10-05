using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Domain.Models;
using IRunes.Domain.ViewModels;
using IRunesApp.Common;
using IRunesServices;
using IRunesServices.Contracts;
using Microsoft.EntityFrameworkCore;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;
using SIS.Infrastructure;

namespace IRunesApp.Controllers
{
    public class AlbumController:BaseController
    {
        private IAlbumService albumService;

        public AlbumController()
        {
            this.albumService = new AlbumService();
        }

        public IHttpResponse Create(IHttpSession session)
        {
            if (!session.IsLoggedIn())
            {
                this.InsertErrorMessage(AppConstants.NotLoggedIn);

                return this.FileViewResponse("Home/index");
            }

            this.SetLoggedInView();

            return this.FileViewResponse("/Albums/Create");
        }

        public IHttpResponse Create(Dictionary<string,string> formData)
        {
            string name = formData["name"];
            string cover = formData["cover"];

            AlbumToCreateViewModel model = new AlbumToCreateViewModel()
            {
                Name = name,
                Cover = cover
            };

            if (!Validation.TryValidate(model))
            {
                return new RedirectResponse("/Albums/all");
            }

            this.albumService.Create(model);

           return new RedirectResponse("/Albums/all");
        }

        public IHttpResponse All(IHttpSession session)
        {
            if (!session.IsLoggedIn())
            {
                return new RedirectResponse("/");
            }

            this.SetLoggedInView();

            List<Album> albums = this.albumService.GetAll();

            if (albums.Count==0)
            {
                this.ViewData["all-albums"] = "There are currently no albums.";

                return this.FileViewResponse("/Albums/all");
            }

            StringBuilder sb = new StringBuilder();

            foreach (Album album in albums)
            {
                sb.Append($"<p><a href =\"/Albums/details?albumId={album.Id}\">{album.Name}</a></p>");
            }

            this.ViewData["all-albums"] = sb.ToString();

            return this.FileViewResponse("/Albums/all");
        }

        public IHttpResponse Details(IHttpSession session, string id)
        {
            if (!session.IsLoggedIn())
            {
                return new RedirectResponse("/");
            }

            Album album = this.albumService.GetById(id);

            if (album==null)
            {
                return new RedirectResponse("/");
            }

            List<Track> tracks = this.albumService.GetAllTracks(album.Id);

            StringBuilder sb = new StringBuilder();
       
            if (tracks.Count==0)
            {
                sb.Append("No tracks");
            }
            else
            {
                for (int i =0;i<tracks.Count;i++)
                {
                    Track track = tracks[i];

                    sb.Append($"<li>{i}. <a href= \"/Tracks/details?albumId={album.Id}&trackId={track.Id}\">{track.Name}</a></li>");
                }
            }

            this.ViewData["all-tracks"] = sb.ToString();
            this.SetLoggedInView();

            return this.FileViewResponse("/Albums/details");
        }

    }
}
