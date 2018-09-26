using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HandMadeHttpServer.ByTheCakeApplication.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:30,MinimumLength =3)]
        public string Name { get; set; }

        [Required]
        [Range(minimum:0,maximum:int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(2000)]
        public string ImageUrl { get; set; }

        public ICollection<OrderProduct> ProductOrders { get; set; } = new List<OrderProduct>();
    }
}