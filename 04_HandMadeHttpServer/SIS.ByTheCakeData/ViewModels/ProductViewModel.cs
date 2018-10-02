using System.Text;

namespace SIS.ByTheCakeData.ViewModels
{
   public class ProductViewModel
    {
        public int Id { get; set; }

        public ProductViewModel(int id, string name, decimal price,string url)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.ImageUrl = url;
        }

        public string Name { get;}

        public decimal Price { get;}

        public string ImageUrl { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<div>{this.Name}</div><br/>");
            sb.AppendLine($"<div>Price: ${this.Price}</div><br/>");
            sb.AppendLine($"<img width=\"200\" height=\"200\" src = \"{this.ImageUrl}\" alt = \"No picture available\" /><br/>");

          return  sb.ToString().TrimEnd('\r','\n');
        }
    }
}
