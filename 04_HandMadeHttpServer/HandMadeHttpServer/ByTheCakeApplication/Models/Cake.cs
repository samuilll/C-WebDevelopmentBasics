using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.ByTheCakeApplication.Models
{
   public class Cake
    {
        public int Id { get; set; }

        public Cake(int id,string name, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public string Name { get;}

        public decimal Price { get;}

        public override string ToString()
        {
            return $"{this.Name} ${this.Price}";
        }
    }
}
