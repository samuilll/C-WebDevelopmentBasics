using System.Collections.Generic;

namespace SIS.ByTheCakeData.Models
{
   public class ShoppingCard
    {
        private List<int> productIds;

        public IReadOnlyList<int> ProductIds
        {
            get
            {
                return (IReadOnlyList<int>)this.productIds;
            }
        } 

        public ShoppingCard()
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
