using SIS.ByTheCakeApp.ViewModels;
using System.Collections.Generic;

namespace SIS.ByTheCakeApp.Models
{
   public class ShoppingCard
    {
        private List<ProductViewModel> orders;

        public IReadOnlyList<ProductViewModel> Orders
        {
            get
            {
                return (IReadOnlyList<ProductViewModel>)this.orders;
            }
        } 

        public ShoppingCard()
        {
            this.orders = new List<ProductViewModel>();
        }

        public void Add(ProductViewModel cake)
        {
            this.orders.Add(cake);
        }

        public void Clear()
        {
            this.orders.Clear();
        }
    }
}
