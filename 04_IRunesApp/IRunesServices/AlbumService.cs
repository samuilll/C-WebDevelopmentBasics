using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRunes.Domain.Models;
using IRunes.Domain.ViewModels;
using IRunesData;
using IRunesServices.Contracts;

namespace IRunesServices
{
   public class AlbumService:IAlbumService
    {
        public void Create(AlbumToCreateViewModel model)
        {
            using (RunesDbContext db = new RunesDbContext())
            {
                Album album = new Album()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Cover = model.Cover
                };

                try
                {
                    db.Albums.Add(album);
                    db.SaveChanges();
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public List<Album> GetAll()
        {
            using (RunesDbContext db = new RunesDbContext())
            {
                return db
                    .Albums
                    .ToList();
            }
        }

        public AlbumDetailsView GetById(string id)
        {
            using (RunesDbContext db = new RunesDbContext())
            {
                Album album =  db
                    .Albums
                    .SingleOrDefault(a => a.Id == id);

                return new AlbumDetailsView()
                {
                    Price = album.Price,
                    Name = album.Name,
                    Id = album.Id,
                    Cover = album.Cover
                };
            }
        }

        public List<Track> GetAllTracks(string albumId)
        {
            using (RunesDbContext db = new RunesDbContext())
            {
                Album album = db.Albums.Find(albumId);

                return album
                    .AlbumTracks
                    .Select(at => at.Track)
                    .ToList();
            }
        }
    }
}
