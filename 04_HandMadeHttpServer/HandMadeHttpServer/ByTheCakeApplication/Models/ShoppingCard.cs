using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.ByTheCakeApplication.Models
{
   public class ShoppingCard
    {
        private List<Cake> orders;

        public IReadOnlyList<Cake> Orders
        {
            get
            {
                return (IReadOnlyList<Cake>)this.orders;
            }
        } 

        public ShoppingCard()
        {
            this.orders = new List<Cake>();
        }

        public void Add(Cake cake)
        {
            this.orders.Add(cake);
        }

        public void Clear()
        {
            this.orders.Clear();
        }
    }
}
