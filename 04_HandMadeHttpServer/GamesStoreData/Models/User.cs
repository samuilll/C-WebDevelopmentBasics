using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIS.Data.Models
{
   public class User
    {
        public User()
        {

        }
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
