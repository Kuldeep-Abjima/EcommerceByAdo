using EcommerceByAdo.Models;

namespace EcommerceByAdo.Interfaces
{
    public interface IMensRepository
    {
        void add(Mens mens);
        List<Mens> GetAll();
        Mens getbyId(Guid id);

        Mens UpdatebyId(Mens men);

        bool DeletebyId(Guid id);
    }
}
