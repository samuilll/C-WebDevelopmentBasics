using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IRunes.Domain.ViewModels
{
    public class AlbumToCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Cover { get; set; }
    }
}
