using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GamesStoreData.Models.ViewModels
{
   public class RegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Password { get; set; }

        [Required]
        [Compare(otherProperty:"Password")]
        public string ConfirmedPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
