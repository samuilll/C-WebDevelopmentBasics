using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GamesStoreData.Attributes;

namespace GamesStoreData.Models.ViewModels
{
   public class GameToAddOrEditViewModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        [GameTitle]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Trailer { get; set; }

        [Url]
        public string ThumbnailUrl { get; set; }

        [Required]
        [StringLength(maximumLength: 100000, MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        [Range(minimum: 0.0, maximum: double.MaxValue)]
        public decimal Size { get; set; }

        [Required]
        [Range(minimum: 0, maximum: double.MaxValue)]
        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
