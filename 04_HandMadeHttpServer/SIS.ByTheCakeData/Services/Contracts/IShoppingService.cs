using System.Collections.Generic;
using SIS.ByTheCakeData.ViewModels;

namespace SIS.ByTheCakeData.Services.Contracts
{
    public interface IShoppingService
    {
        ICollection<ProductViewModel> GetOrderProducts(ICollection<int> productIds);

        void CreateOrder(ICollection<int> productIds,int userId);

        OrderViewModel GetOrderById(int orderId);

    }
}
