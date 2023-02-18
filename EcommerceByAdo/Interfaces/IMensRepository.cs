using EcommerceByAdo.Models;
using EcommerceByAdo.ViewModel;

namespace EcommerceByAdo.Interfaces
{
    public interface IMensRepository
    {
        void add(Mens mens);
        List<Mens> GetAll();
        Mens getbyId(long id);
    }
}
