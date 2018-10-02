using System.Collections.Generic;
using System.Text;

namespace SIS.ByTheCakeData.ViewModels
{
  public  class ProductsCollectionView
    {
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var product in this.Products)
            {
                sb.AppendLine($"<div>{product}</div>");
            }

            return sb.ToString().TrimEnd('\n', '\r');
        }
    }
}
