using System.ComponentModel.DataAnnotations;

namespace IRunes.Domain.ViewModels
{
   public class RegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Username { get; set; }

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
