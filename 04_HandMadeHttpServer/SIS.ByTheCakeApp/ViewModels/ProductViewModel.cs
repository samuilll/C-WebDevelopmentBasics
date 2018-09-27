namespace SIS.ByTheCakeApp.ViewModels
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
            return $"{this.Name} ${this.Price}";
        }
    }
}
