using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.ByTheCakeApplication.Models
{
   public class Cake
    {
        public Cake(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }

        public string Name { get;}

        public decimal Price { get;}

        public override string ToString()
        {
            return $"{this.Name},{this.Price}";
        }
    }
}
