using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIS.Data.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:30,MinimumLength =3)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Trailer { get; set; }

        [Required]
        [StringLength(maximumLength: 2000)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public decimal Size { get; set; }

        [Required]
        [Range(minimum:0,maximum:int.MaxValue)]
        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }


        public ICollection<OrderGame> Orders { get; set; } = new List<OrderGame>();
    }
}