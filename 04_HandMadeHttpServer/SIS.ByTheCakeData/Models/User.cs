﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIS.ByTheCakeData.Models
{
   public class User
    {
        public User()
        {

        }
        public int Id { get; set; }

        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
