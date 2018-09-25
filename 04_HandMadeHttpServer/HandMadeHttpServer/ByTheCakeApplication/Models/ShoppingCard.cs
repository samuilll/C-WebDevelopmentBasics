using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.ByTheCakeApplication.Models
{
   public class ShoppingCard
    {
        public List<Cake> Orders { get; private set; } = new List<Cake>();

        public ShoppingCard()
        {
            this.Orders = new List<Cake>();
        }
    }
}
