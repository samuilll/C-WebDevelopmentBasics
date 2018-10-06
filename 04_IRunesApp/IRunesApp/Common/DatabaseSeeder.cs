using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Domain.Models;
using IRunesData;

namespace IRunesApp.Common
{
   public class DatabaseSeeder
    {
        public void Seed(RunesDbContext db)
        {
            List<Album> albums = new List<Album>()
                    {
                        new Album()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Master Of Puppets",
                            Cover =
                                "https://upload.wikimedia.org/wikipedia/en/thumb/b/b2/Metallica_-_Master_of_Puppets_cover.jpg/220px-Metallica_-_Master_of_Puppets_cover.jpg"
                        },
                        new Album()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Play the game",
                            Cover = "https://images.eil.com/large_image/QUEEN_PLAY%2BTHE%2BGAME-291209.jpg",
                        }
                    };

            var firstAlbumTracks = new HashSet<AlbumTrack>()
                    {
                        new AlbumTrack()
                        {
                            AlbumId = albums[0].Id,
                            Track = new Track()
                            {
                                Name = "Battery",
                                Link = "https://www.youtube.com/embed/UipTt-qqZOE",
                                Price = 4.56M
                            }
                        },
                         new AlbumTrack()
                        {
                            AlbumId = albums[0].Id,
                            Track = new Track()
                            {
                                Name = "Master of puppets",
                                Link = "https://www.youtube.com/embed/xnKhsTXoKCI",
                                Price = 6.56M
                            }
                        }, new AlbumTrack()
                        {
                            AlbumId = albums[0].Id,
                            Track = new Track()
                            {
                                Name = "The thing that should not  be",
                                Link = "https://www.youtube.com/embed/DuWtFk1Lue4",
                                Price = 4.56M
                            }
                        },
                        new AlbumTrack()
                        {
                            AlbumId = albums[0].Id,
                            Track = new Track()
                            {
                                Name = "Sanataoritum",
                                Link = "https://www.youtube.com/embed/WElvEZj0Ltw",
                                Price = 4.56M
                            }
                        },
                    };

            var secondAlbumTracks = new HashSet<AlbumTrack>()
                    {
                        new AlbumTrack()
                        {
                            AlbumId = albums[1].Id,
                            Track = new Track()
                            {
                                Name = "Play the game",
                                Link = "https://www.youtube.com/embed/LS1RXZ6qpLc",
                                Price = 4.56M
                            }
                        },
                        new AlbumTrack()
                        {
                            AlbumId = albums[1].Id,
                            Track = new Track()
                            {
                                Name = "Another one bites to dust",
                                Link = "https://www.youtube.com/embed/rY0WxgSXdEE",
                                Price = 4.56M
                            }
                        }, new AlbumTrack()
                        {
                            AlbumId = albums[1].Id,
                            Track = new Track()
                            {
                                Name = "Spread your wings",
                                Link = "https://www.youtube.com/embed/uyd6OLyhPJo",
                                Price = 6.56M
                            }
                        }, new AlbumTrack()
                        {
                            AlbumId = albums[0].Id,
                            Track = new Track()
                            {
                                Name = "The thing that should not  be",
                                Link = "https://www.youtube.com/embed/DuWtFk1Lue4",
                                Price = 4.56M
                            }
                        }
                    };

            albums[0].AlbumTracks = firstAlbumTracks;
            albums[1].AlbumTracks = secondAlbumTracks;

            db.Albums.AddRange(albums);
            db.SaveChanges();
        }
    }
}
