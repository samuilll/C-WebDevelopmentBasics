using System;
using IRunesApp.Controllers;
using IRunesData;
using IRunesServices;
using Microsoft.EntityFrameworkCore;
using SIS.WebServer.Contracts;
using SIS.WebServer.Routing.Contracts;

namespace IRunesApp
{
    public class IRunesApplication:IApplication
    {
        public void InitializeDatabase()
        {
            using (RunesDbContext db = new RunesDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddAnonymousPath("/");

            appRouteConfig.AddAnonymousPath("/Users/register");

            appRouteConfig.AddAnonymousPath("/Users/login");

            appRouteConfig.Get(
                "/",
                req=>new HomeController().Index(req));

            appRouteConfig.Get(
                "/Users/login",
                req => new AccountController().Login(req.Session));

            appRouteConfig.Post(
                "/Users/login",
                req => new AccountController().Login(req.Session,req.FormData));

            appRouteConfig.Get(
                "/Users/register",
                req => new AccountController().Register(req.Session));

            appRouteConfig.Post(
                "/Users/register",
                req => new AccountController().Register(req.Session,req.FormData));

            appRouteConfig.Get(
                "/Users/logout",
                req => new AccountController().Logout(req.Session));

            appRouteConfig.Get(
                "/Albums/create",
                req => new AlbumController().Create(req.Session));

            appRouteConfig.Post(
                "/Albums/create",
                req => new AlbumController().Create(req.FormData));

            appRouteConfig.Get(
                "/Albums/all",
                req => new AlbumController().All(req.Session));

            appRouteConfig.Get(
                "/Albums/details?albumId={(?<albumId>[A-Za-z0-9-]+)}",
                req => new AlbumController().Details(req.Session,req.UrlParameters["albumId"]));

            appRouteConfig.Get(
                "/Tracks/create?albumId={(?<id>.+)}",
                req => new TrackController().Create(req.UrlParameters));

            appRouteConfig.Post(
                "/Tracks/create?albumId={(?<id>.+)}",
                req => new TrackController().Create(req));

            appRouteConfig.Get(
                "/Tracks/details?albumId={(?<id>[A-Za-z0-9]+)}&trackId={(?<id>[A-Za-z0-9]+)}}",
                req => new TrackController().Details(req));

        }
    }
}
