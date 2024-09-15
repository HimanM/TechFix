using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Service.InventoryServiceReference;

namespace Service
{
    /// <summary>
    /// Summary description for OrderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class OrderService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void AddOrder(int supplierId, decimal totalAmount, List<OrderItem> orderItems)
        {
            string status = "Pending";
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int orderId;
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Orders (supplier_id, total_amount, status) VALUES (@supplierId, @totalAmount, @status); SELECT SCOPE_IDENTITY();", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@supplierId", supplierId);
                            cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
                            cmd.Parameters.AddWithValue("@status", status);
                            orderId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        using (SqlCommand trackCmd = new SqlCommand("INSERT INTO Order_Tracking (order_id, status) VALUES (@orderId, @status)", conn, transaction))
                        {
                            trackCmd.Parameters.AddWithValue("@orderId", orderId);
                            trackCmd.Parameters.AddWithValue("@status", status);
                            trackCmd.ExecuteNonQuery();
                        }
                        //Inventory Update
                        InventoryServiceReference.InventoryServiceSoapClient inventoryService = new InventoryServiceReference.InventoryServiceSoapClient();
                        foreach (var item in orderItems)
                        {
                            inventoryService.RemoveFromInventory(item.ProductId, item.QuantityOrdered);

                            using (SqlCommand itemCmd = new SqlCommand("INSERT INTO Order_Items (order_id, product_id, quantity_ordered, price_per_unit) VALUES (@orderId, @productId, @quantityOrdered, @pricePerUnit)", conn, transaction))
                            {
                                itemCmd.Parameters.AddWithValue("@orderId", orderId);
                                itemCmd.Parameters.AddWithValue("@productId", item.ProductId);
                                itemCmd.Parameters.AddWithValue("@quantityOrdered", item.QuantityOrdered);
                                itemCmd.Parameters.AddWithValue("@pricePerUnit", item.PricePerUnit);
                                itemCmd.ExecuteNonQuery();
                            }
                        }

                        using (SqlCommand totalCmd = new SqlCommand("UPDATE Orders SET total_amount = (SELECT SUM(quantity_ordered * price_per_unit) FROM Order_Items WHERE order_id = @orderId) WHERE id = @orderId", conn, transaction))
                        {
                            totalCmd.Parameters.AddWithValue("@orderId", orderId);
                            totalCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        [WebMethod]
        public String UpdateOrderStatus(int orderId, string status)
        {

            SqlConnection conn = Database.GetConnection();
            conn.Open();
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    // Update from Order_Tracking table
                    using (SqlCommand cmd = new SqlCommand("UPDATE Order_Tracking SET status = @status WHERE order_id = @orderId", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.ExecuteNonQuery();
                    }

                    // Update from Orders table
                    using (SqlCommand cmd = new SqlCommand("UPDATE Orders SET status = @status WHERE id = @orderId", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.ExecuteNonQuery();
                    }


                    // Commit transaction if both commands succeed
                    transaction.Commit();
                    return "Success";
                }
                catch (Exception ex)
                {
                    // Rollback if there is an error
                    transaction.Rollback();
                    throw; // or handle the error accordingly
                }
            }
        }

        [WebMethod]
        public void DeleteOrder(int orderId)
        {
            SqlConnection conn = Database.GetConnection();
            conn.Open();
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    // Delete from Order_Items table
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Order_Items WHERE order_id = @orderId", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.ExecuteNonQuery();
                    }

                    // Delete from Order_Tracking table
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Order_Tracking WHERE order_id = @orderId", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.ExecuteNonQuery();
                    }

                    // Delete from Orders table
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Orders WHERE id = @orderId", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
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

        [WebMethod]
        public DataTable GetOrderById(int orderId)
        {
            DataTable dt = new DataTable();
            dt.TableName = "OrderById";
            using (SqlConnection conn = Database.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE id = @orderId", conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        [WebMethod]
        public DataTable GetAllOrders()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Orders";
            using (SqlConnection conn = Database.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT o.id AS OrderId, s.name AS SupplierName, o.total_amount AS TotalAmount, o.status AS Status FROM Orders o JOIN Suppliers s ON o.supplier_id = s.id", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        [WebMethod]
        public void AddOrderItem(int orderId, int productId, int quantityOrdered, decimal pricePerUnit)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Order_Items (order_id, product_id, quantity_ordered, price_per_unit) VALUES (@orderId, @productId, @quantityOrdered, @pricePerUnit)", conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.Parameters.AddWithValue("@productId", productId);
                    cmd.Parameters.AddWithValue("@quantityOrdered", quantityOrdered);
                    cmd.Parameters.AddWithValue("@pricePerUnit", pricePerUnit);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [WebMethod]
        public DataTable GetOrderItems(int orderId)
        {
            DataTable dt = new DataTable();
            dt.TableName = "OrderItems";
            using (SqlConnection conn = Database.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT p.name AS ProductName, oi.quantity_ordered AS QuantityOrdered, oi.price_per_unit AS PricePerUnit, (oi.quantity_ordered * oi.price_per_unit) AS TotalPrice FROM Order_Items oi JOIN Products p ON oi.product_id = p.id WHERE oi.order_id = @orderId", conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
    [Serializable]
    public class OrderItem
    {
        public int ProductId { get; set; }
        public int QuantityOrdered { get; set; }
        public decimal PricePerUnit { get; set; }
        public String ProductName { get; set; }


        public OrderItem(int productId, int quantityOrdered, decimal pricePerUnit, String productName)
        {
            ProductId = productId;
            QuantityOrdered = quantityOrdered;
            PricePerUnit = pricePerUnit;
            ProductName = productName;
        }

        public OrderItem() { }
    }
}
