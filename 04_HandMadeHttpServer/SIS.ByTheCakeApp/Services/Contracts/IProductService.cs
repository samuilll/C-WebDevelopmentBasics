using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.ByTheCakeApp.Services.Contracts
{
    using ViewModels;

   public  interface IProductService
    {
        bool Add(string name, string price, string url);

        ICollection<ProductViewModel> GetAllBySearchedTerm(string searchTerm);

        ICollection<ProductViewModel> GetAllByOrderId(int orderId);

        ProductViewModel GetByIdOrNull(int id);

        bool ExistProduct(int id);
    }
}
