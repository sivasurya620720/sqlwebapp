using sqlapp.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "appdbserveryalamarty.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_pwd = "P@ssword1234";
        private static string db_database = "app-db";

        private SqlConnection getconnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID=db_user;
            builder.Password=db_pwd;
            builder.InitialCatalog= db_database;

            return new SqlConnection(builder.ConnectionString);
            
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn= getconnection();

            List <Product> product_lst = new List<Product> ();

            string statement = "Select ProductID, ProductName, Quantity from AllProducts";
            conn.Open();

            SqlCommand cmd=new SqlCommand(statement, conn);

            using(SqlDataReader reader = cmd.ExecuteReader()) 
            { 
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32 (2),
                    };
                    product_lst.Add(product);
                }
            }
            conn.Close();
            return product_lst;

        }

    }
}
