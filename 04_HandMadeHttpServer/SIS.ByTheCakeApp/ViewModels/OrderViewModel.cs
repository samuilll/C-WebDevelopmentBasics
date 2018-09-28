using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.ByTheCakeApp.ViewModels
{
   public class OrderViewModel
    {
        public int OrderId { get; set; }

        public DateTime CreationDate { get; set; }

        public decimal TotalSum { get; set; }

    }
}
