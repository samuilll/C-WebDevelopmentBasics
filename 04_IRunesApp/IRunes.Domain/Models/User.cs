using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IRunes.Domain.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 300, MinimumLength = 3)]
        public string HashedPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
