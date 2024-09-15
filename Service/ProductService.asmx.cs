using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace Service
{
    /// <summary>
    /// Summary description for ProductService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProductService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void AddProduct(string name, string description, decimal price, int supplierId, string category)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Products (name, description, price, supplier_id, category) VALUES (@name, @description, @price, @supplierId, @category)", con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@supplierId", supplierId);
                cmd.Parameters.AddWithValue("@category", category);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Function to Update a Product
        [WebMethod]
        public void UpdateProduct(int productId, string name, string description, decimal price, int supplierId, string category)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Products SET name=@name, description=@description, price=@price, supplier_id=@supplierId, category=@category WHERE id=@productId", con);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@supplierId", supplierId);
                cmd.Parameters.AddWithValue("@category", category);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Function to Delete a Product
        [WebMethod]
        public void DeleteProduct(int productId)
        {
            SqlConnection conn = Database.GetConnection();
            conn.Open();
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    // Delete from Inventory table
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Inventory WHERE product_id = @productId", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.ExecuteNonQuery();
                    }

                    // Delete from Products table
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE id=@productId", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.ExecuteNonQuery();
                    }

                    // Commit transaction if both commands succeed
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback if there is an error
                    transaction.Rollback();
                    throw; // or handle the error accordingly
                }
            }
        }

        // Function to Get Product By ID
        [WebMethod]
        public DataSet GetProductById(int productId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Products WHERE id=@productId", con);
                da.SelectCommand.Parameters.AddWithValue("@productId", productId);

                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
        }

        [WebMethod]
        public DataSet GetProductBySupplierId(int supplierId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Products WHERE supplier_id=@supplier_id", con);
                da.SelectCommand.Parameters.AddWithValue("@supplier_id", supplierId);

                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
        }

        // Function to Get All Products
        [WebMethod]
        public DataSet GetAllProducts()
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT 
                    p.*, 
                    s.name AS SupplierName
                FROM 
                    Products p
                JOIN 
                    Suppliers s 
                ON 
                    p.supplier_id = s.id", con);

                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
        }

        // Function to Update Product Stock in Inventory
        [WebMethod]
        public void UpdateProductStock(int productId, int supplierId, int quantityAvailable)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Inventory SET quantity_available=@quantityAvailable WHERE product_id=@productId AND supplier_id=@supplierId", con);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@supplierId", supplierId);
                cmd.Parameters.AddWithValue("@quantityAvailable", quantityAvailable);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Function to Get Product Inventory
        [WebMethod]
        public DataSet GetProductInventory(int productId)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Inventory WHERE product_id=@productId", con);
                da.SelectCommand.Parameters.AddWithValue("@productId", productId);

                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
        }

        //testing
        [WebMethod]
        public DataSet GetAllSuppliers()
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT supplier_id FROM Products", con);

                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
        }
        
    }
}
