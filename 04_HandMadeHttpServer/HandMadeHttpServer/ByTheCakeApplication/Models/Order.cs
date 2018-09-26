using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HandMadeHttpServer.ByTheCakeApplication.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}