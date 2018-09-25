using HandMadeHttpServer.ByTheCakeApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HandMadeHttpServer.ByTheCakeApplication.Data
{
  public  class CakeManager
    {
        private const string CakesDatabasePath = "ByTheCakeApplication/Data/database.csv";

        public ICollection<Cake> Cakes { get; }

        public CakeManager()
        {
            this.Cakes = this.GetAll();
        }

        private  ICollection<Cake> GetAll()
        {
            var allCakes = File
                   .ReadAllLines(CakesDatabasePath)
                   .Where(l => l.Contains(','))
                   .Select(l => l.Split(',', StringSplitOptions.RemoveEmptyEntries))
                   .Select(l => new Cake(id: int.Parse(l[0]), name: l[1], price: decimal.Parse(l[2])))
                   .ToList();

            return allCakes;
        }

        public void Add(string name,string priceAsString)
        {
            const string databaseFile = CakesDatabasePath;

            var streamReader = new StreamReader(databaseFile);

            var id = streamReader.ReadToEnd().Split(Environment.NewLine).Length;

            var price = decimal.Parse(priceAsString);

            var cake = new Cake(id, name, price);

            streamReader.Dispose();

            using (var streamWriter = new StreamWriter(databaseFile, true))
            {
                streamWriter.WriteLine($"{id},{cake.Name},{cake.Price}");
            }
        }
        public  IEnumerable<Cake> GetAllSearched(string searchedName)
        {
            return this.Cakes
                .Where(c => c.Name.Contains(searchedName))
                .ToList();
        }

        public  Cake FindById(int id)
        {
            return this.Cakes
                           .Where(c => c.Id==id)
                           .FirstOrDefault();
        }
    }
}
