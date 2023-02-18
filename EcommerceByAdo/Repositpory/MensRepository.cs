using EcommerceByAdo.Interfaces;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using EcommerceByAdo.Models;
using EcommerceByAdo.Services;
using EcommerceByAdo.Data.Enums;
using EcommerceByAdo.ViewModel;

namespace EcommerceByAdo.Repositpory
{
    public class MensRepository : IMensRepository
    {
        private readonly IConfiguration _configuration;

        public MensRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


       

        public List<Mens> GetAll()
        {
           var mens = new List<Mens>();
            string cs = _configuration.GetConnectionString("AppDbConnection");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_getAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var men = new Mens()
                    {
                        Id = Convert.ToInt64(reader.GetValue(0).ToString()),
                        Identifier = reader.GetValue(1).GetType().GUID,
                        CName = reader.GetValue(2).ToString(),
                        CImage = reader.GetValue(3).ToString(),
                        Rate = Convert.ToInt32(reader.GetValue(4).ToString()),
                        Category = (Data.Enums.Category?)Convert.ToInt32(reader.GetValue(5).ToString()),
                        UserId = reader.GetValue(6).GetType().GUID,

                    };
                    mens.Add(men);
                    
                }
                con.Close();
            }
                return mens;
        }

        public void add(Mens mens)
        {
            string cs = _configuration.GetConnectionString("AppDbConnection");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_men_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Identifier", mens.Identifier);
                cmd.Parameters.AddWithValue("@Name", mens.CName);
                cmd.Parameters.AddWithValue("@Image", mens.CImage);
                cmd.Parameters.AddWithValue("@Rate", mens.Rate);
                cmd.Parameters.AddWithValue("@category", mens.Category);
                cmd.Parameters.AddWithValue("@userId", mens.UserId);
                con.Open();
                cmd.ExecuteReader();
                con.Close();
            }
            

        }
        public Mens getbyId(long id)
        {
            var men = new Mens();
            string cs = _configuration.GetConnectionString("AppDbConnection");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_getbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    men.Id = Convert.ToInt64(reader.GetValue(0).ToString());
                    men.Identifier = reader.GetValue(1).GetType().GUID;
                    men.CName = reader.GetValue(2).ToString();
                    men.CImage = reader.GetValue(3).ToString();
                    men.Rate = Convert.ToInt32(reader.GetValue(4).ToString());
                    men.Category = (Data.Enums.Category?)Convert.ToInt32(reader.GetValue(5).ToString());
                    men.UserId = reader.GetValue(6).GetType().GUID;

                };

                con.Close();

            }
            return men;

        }

        //TODO

        //public Mens UpdatebyId(long id)
        //{

        //}





    }
}
