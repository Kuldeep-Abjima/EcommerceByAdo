using EcommerceByAdo.Models;
using EcommerceByAdo.ViewModel;

namespace EcommerceByAdo.Interfaces
{
    public interface IProductATCRepository
    {
        List<AddToCartViewModel> GetAll();
        bool add(Guid id);
        bool DeleteAll(Guid id);
    }
}
