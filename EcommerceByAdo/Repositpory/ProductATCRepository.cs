using EcommerceByAdo.Interfaces;
using EcommerceByAdo.Models;
using EcommerceByAdo.ViewModel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Contracts;

namespace EcommerceByAdo.Repositpory
{
    public class ProductATCRepository : IProductATCRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDataBaseConnection _baseConnection;
        private readonly IMensRepository _mens;

        public ProductATCRepository(IDataBaseConnection BaseConnection, IMensRepository mens)
        {
            
            _baseConnection = BaseConnection;
            _mens = mens;
        }



        public List<AddToCartViewModel> GetAll()
        {
            List<AddToCartViewModel> products = new List<AddToCartViewModel>(); 

            using (SqlConnection con = new SqlConnection(_baseConnection.connection()))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllProducts",con);
                
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Guid> ids = new List<Guid>();
                while (reader.Read())
                {
                    var id = (Guid)reader["MensGuid"];
                    ids.Add(id);
                }
                con.Close();
                foreach (var items in ids)
                {
                    var men = _mens.getbyId(items);
                    SqlCommand com2 = new SqlCommand("sp_GetQuantity", con);
                    
                    con.Open();
                    com2.CommandType= CommandType.StoredProcedure;
                    
                    com2.Parameters.AddWithValue("@MensGuid", items);
                    var quan = com2.ExecuteScalar();
                    

                    con.Close();
                    SqlCommand com3 = new SqlCommand("get_ProductCartID", con);
                    con.Open();
                    com3.CommandType = CommandType.StoredProcedure;
                    com3.Parameters.AddWithValue("@MensGuid", items);
                    var pid = com3.ExecuteScalar();
                    con.Close();
                    var product = new AddToCartViewModel
                    {
                        Id = (Guid)pid,
                        Name = men.CName,
                        Image = men.CImage,
                        Rate = men.Rate,
                        quantity = (int)quan
                    };
                    products.Add(product);
                }
                return products;

            }
        }
       
        public bool add(Guid id)
        {
            var men = _mens.getbyId(id);
            if (men != null)
            {
                var product = new ProductATC();
               
                using (SqlConnection con = new SqlConnection(_baseConnection.connection()))
                {
                    SqlCommand cmd = new SqlCommand("sp_AddToCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductCartId", product.ProductCartID);
                    cmd.Parameters.AddWithValue("@MensGuid", men.Identifier);
                    cmd.Parameters.AddWithValue("@UserId", product.UserId);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                    cmd.Parameters.AddWithValue("@Date", product.Date);
                    cmd.Parameters.AddWithValue("@orderId", product.OrderID);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    return result > 0 ? true : false;
                }
            }

            return false;
            
        }

        public bool DeleteAll(Guid id)
        {
            using (SqlConnection con = new SqlConnection(_baseConnection.connection()))
            {
                SqlCommand cmd = new SqlCommand("sp_Delete_Productbyall", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductCartId",id);
                con.Open() ;
                var result = cmd.ExecuteNonQuery();
                con.Close();
                return result > 0 ? true : false;

            }
        }

        public bool AddProductQuantity(Guid id)
        {
            using(SqlConnection con = new SqlConnection(_baseConnection.connection()))
            {
                SqlCommand cmd = new SqlCommand("sp_add_product_quanity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductCartId", id);
                con.Open() ;
                var result = cmd.ExecuteNonQuery();
                con.Close();
                return result > 0 ? true : false;
            }

        }
        public bool LessProductQuantity(Guid id)
        {
            using (SqlConnection con = new SqlConnection(_baseConnection.connection()))
            {
                SqlCommand cmd = new SqlCommand("sp_Less_product_quanity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                var result = cmd.ExecuteNonQuery();
                con.Close();
                return result > 0 ? true : false;
            }

        }
    }
}
