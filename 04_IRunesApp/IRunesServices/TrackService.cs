using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRunes.Domain.Models;
using IRunesData;
using IRunesServices.Contracts;
using SIS.Infrastructure;

namespace IRunesServices
{
   public class TrackService:ITrackService
    {
        public bool Create(string albumId, string name, string link, decimal price)
        {
            using (RunesDbContext db = new RunesDbContext())
            {
                Track track = new Track()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Link = link,
                    Price = price,
                };

                if (!Validation.TryValidate(track))
                {
                    return false;
                }

                AlbumTrack albumTrack = new AlbumTrack()
                {
                    Track = track,
                    AlbumId = albumId
                };

                db.AlbumsTracks.Add(albumTrack);

                db.SaveChanges();

                return true;
            }
        }

        public Track GetById(string trackId)
        {
            using (RunesDbContext db = new RunesDbContext())
            {
                return db
                    .Tracks
                    .SingleOrDefault(t => t.Id == trackId);
            }

        }
    }
}
