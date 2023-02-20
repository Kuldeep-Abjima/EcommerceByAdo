using EcommerceByAdo.Interfaces;

namespace EcommerceByAdo.Data
{
    public class DataBaseConnection : IDataBaseConnection
    {
        private readonly IConfiguration _configuration;

        public DataBaseConnection(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public string connection()
        {
            return _configuration.GetConnectionString("AppDbConnection");
        }
    }
}
