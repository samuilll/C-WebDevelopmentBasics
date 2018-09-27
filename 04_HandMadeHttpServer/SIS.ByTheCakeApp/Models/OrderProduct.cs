using System.ComponentModel.DataAnnotations;

namespace SIS.ByTheCakeApp.Models
{
   public class OrderProduct
    {
        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
