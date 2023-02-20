using EcommerceByAdo.Interfaces;
using System.Data.SqlClient;
using System.Data;
using EcommerceByAdo.Models;

namespace EcommerceByAdo.Repositpory
{
    public class MensRepository : IMensRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDataBaseConnection _baseConnection;

        public MensRepository(IDataBaseConnection baseConnection)
        {
            _baseConnection = baseConnection;
        }


       

        public List<Mens> GetAll()
        {
           var mens = new List<Mens>();
            //string cs = _configuration.GetConnectionString("AppDbConnection");
            using (SqlConnection con = new SqlConnection(_baseConnection.connection()))
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
                        Identifier = (Guid)reader["identifier"],
                        CName = reader.GetValue(2).ToString(),
                        CImage = reader.GetValue(3).ToString(),
                        Rate = Convert.ToInt32(reader.GetValue(4).ToString()),
                        Category = (Data.Enums.Category?)Convert.ToInt32(reader.GetValue(5).ToString()),
                        UserId = (Guid)reader["UsersId"],

                    };
                    mens.Add(men);
                    
                }
                con.Close();
            }
                return mens;
        }

        public void add(Mens mens)
        {
            //string cs = _configuration.GetConnectionString("AppDbConnection");
            using (SqlConnection con = new SqlConnection(_baseConnection.connection()))
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
        public Mens getbyId(Guid id)
        {
            var men = new Mens();
            //string cs = _configuration.GetConnectionString("AppDbConnection");
            using (SqlConnection con = new SqlConnection(_baseConnection.connection()))
            {
                SqlCommand cmd = new SqlCommand("sp_getbyGuidId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@guid", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    men.Id = Convert.ToInt64(reader.GetValue(0).ToString());
                    men.Identifier = (Guid)reader["identifier"];
                    men.CName = reader.GetValue(2).ToString();
                    men.CImage = reader.GetValue(3).ToString();
                    men.Rate = Convert.ToInt32(reader.GetValue(4).ToString());
                    men.Category = (Data.Enums.Category?)Convert.ToInt32(reader.GetValue(5).ToString());
                    men.UserId = (Guid)reader["UsersId"];

                };

                con.Close();

            }
            return men;

        }

        //TODO

        public Mens UpdatebyId(Mens men)
        {
            //string cs = _configuration.GetConnectionString("AppDbConnection");
            using (SqlConnection con = new SqlConnection(_baseConnection.connection()))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateMens", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", men.Id);
                cmd.Parameters.AddWithValue("@identifier", men.Identifier);
                cmd.Parameters.AddWithValue("@Name", men.CName);
                cmd.Parameters.AddWithValue("@Image", men.CImage);
                cmd.Parameters.AddWithValue("@Rate", men.Rate);
                cmd.Parameters.AddWithValue("@category", men.Category);
                cmd.Parameters.AddWithValue("@userId", men.UserId);
                con.Open();
                cmd.ExecuteReader();
                con.Close() ;
            }
            return men;

        }


        public bool DeletebyId(Guid id)
        {
            //string cs = _configuration.GetConnectionString("AppDbConnection");
            using (SqlConnection con = new SqlConnection(_baseConnection.connection()))
            {
                SqlCommand cmd = new SqlCommand("Sp_DeleteManbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Identifier", id);
                con.Open();
                int val = cmd.ExecuteNonQuery();
                con.Close();

                return val > 0 ? true : false;

            }


        }


    }
}
