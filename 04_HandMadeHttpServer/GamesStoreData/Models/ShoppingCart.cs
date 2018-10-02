using System.Collections.Generic;

namespace GamesStoreData.Models
{
   public class ShoppingCart
    {
        private List<int> productIds;

        public IReadOnlyList<int> ProductIds
        {
            get
            {
                return (IReadOnlyList<int>)this.productIds;
            }
        } 

        public ShoppingCart()
        {
            this.productIds = new List<int>();
        }

        public void Add(int productId)
        {
            this.productIds.Add(productId);
        }

        public void Clear()
        {
            this.productIds.Clear();
        }
    }
}
