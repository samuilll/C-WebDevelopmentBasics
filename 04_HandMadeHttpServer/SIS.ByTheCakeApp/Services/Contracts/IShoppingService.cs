using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.ByTheCakeApp.Services.Contracts
{
    using ViewModels;

   public interface IShoppingService
    {
        ICollection<ProductViewModel> GetOrderProducts(ICollection<int> productIds);

        void CreateOrder(ICollection<int> productIds,int userId);

        OrderViewModel GetOrderById(int orderId);

    }
}
