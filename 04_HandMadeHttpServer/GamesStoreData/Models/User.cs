using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamesStoreData.Models
{
   public class User
    {
        public User()
        {

        }
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
