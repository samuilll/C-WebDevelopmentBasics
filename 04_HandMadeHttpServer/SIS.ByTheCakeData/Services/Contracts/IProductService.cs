using System.Collections.Generic;
using SIS.ByTheCakeData.ViewModels;

namespace SIS.ByTheCakeData.Services.Contracts
{
    public  interface IProductService
    {
        bool Add(string name, string price, string url);

        ICollection<ProductViewModel> GetAllBySearchedTerm(string searchTerm);

        ICollection<ProductViewModel> GetAllByOrderId(int orderId);

        ProductViewModel GetByIdOrNull(int id);

        bool ExistProduct(int id);
    }
}
