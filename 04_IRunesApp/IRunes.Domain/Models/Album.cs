using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IRunes.Domain.Models
{
    public class Album
    {
        public Album()
        {
            this.AlbumTracks = new HashSet<AlbumTrack>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Cover { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Price => CalculatePrice(this.AlbumTracks);

        public virtual ICollection<AlbumTrack> AlbumTracks { get; set; }

        private decimal CalculatePrice(ICollection<AlbumTrack> albumTracks)
        {
            decimal price = albumTracks.Select(at=>at.Track.Price).Sum();

            if (price==0)
            {
                return price;
            }

            price -= 13 * 100 / price;

            return price;
        }

    }
}