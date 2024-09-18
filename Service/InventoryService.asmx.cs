using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Service
{
    /// <summary>
    /// Summary description for InventoryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InventoryService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public string EditInventoryQuantity(int Id, int newQuantity)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Inventory SET quantity_available = @quantity, last_updated = GETDATE() WHERE id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@quantity", newQuantity);
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return "Inventory updated successfully.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Function 2: Remove specific quantity from inventory
        [WebMethod]
        public string RemoveFromInventory(int Id, int quantityToRemove)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Inventory SET quantity_available = quantity_available - @quantityToRemove, last_updated = GETDATE() WHERE id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@quantityToRemove", quantityToRemove);
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return "Quantity removed successfully from inventory.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Function 3: Add a new product to inventory
        [WebMethod]
        public string AddProductToInventory(int productId, int supplierId, int quantityAvailable)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    // Check if the record already exists in the Inventory table
                    string query = @"
                        IF EXISTS (SELECT 1 FROM Inventory WHERE product_id = @productId AND supplier_id = @supplierId)
                        BEGIN
                            -- Update existing record
                            UPDATE Inventory 
                            SET quantity_available = @quantityAvailable, last_updated = GETDATE() 
                            WHERE product_id = @productId AND supplier_id = @supplierId
                        END
                        ELSE
                        BEGIN
                            -- Insert new record
                            INSERT INTO Inventory (product_id, supplier_id, quantity_available, last_updated) 
                            VALUES (@productId, @supplierId, @quantityAvailable, GETDATE())
                        END";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.Parameters.AddWithValue("@supplierId", supplierId);
                        cmd.Parameters.AddWithValue("@quantityAvailable", quantityAvailable);
                        cmd.ExecuteNonQuery();
                    }
                }
                return "Product added to inventory successfully.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Function 4: Get inventory by product ID (Optional Utility)
        [WebMethod]
        public DataSet GetInventoryByProductId(int productId)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Inventory WHERE product_id = @productId";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@productId", productId);
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            return ds;
        }

        //get all inv data
        [WebMethod]
        public DataSet GetAllInventory()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    i.product_id,
                    p.name AS ProductName,
                    s.name AS SupplierName,
                    i.quantity_available,
                    i.last_updated
                FROM 
                    Inventory i
                INNER JOIN 
                    Products p ON i.product_id = p.id
                INNER JOIN 
                    Suppliers s ON i.supplier_id = s.id";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            return ds;
        }

        [WebMethod]
        public DataSet GetAllInventoryProducts()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                    SELECT 
                        i.id,
                        i.product_id,
                        p.name AS ProductName,
                        (p.name + ' By ' + s.name) AS DetailProduct
                    FROM 
                        Inventory i
                    INNER JOIN 
                        Products p ON i.product_id = p.id
                    INNER JOIN 
                        Suppliers s ON i.supplier_id = s.id";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            return ds;
        }
    }
}
