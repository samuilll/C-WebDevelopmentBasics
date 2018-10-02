using System;

namespace SIS.ByTheCakeData.ViewModels
{
   public class OrderViewModel
    {
        public int OrderId { get; set; }

        public DateTime CreationDate { get; set; }

        public decimal TotalSum { get; set; }

    }
}
