using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IRunes.Domain.Models
{
    public class Track 
    {
        public Track()
        {
            this.TrackAlbums = new HashSet<AlbumTrack>();
        }
        
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<AlbumTrack> TrackAlbums { get; set; }

    }
}