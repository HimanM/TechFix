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
    /// Summary description for QuotationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class QuotationService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void AddQuotation(int supplierId, int productId, int quantityRequested, decimal priceOffered)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Quotations (supplier_id, product_id, quantity_requested, price_offered, status) VALUES (@SupplierId, @ProductId, @QuantityRequested, @PriceOffered, 'Pending')", conn);
                cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.Parameters.AddWithValue("@QuantityRequested", quantityRequested);
                cmd.Parameters.AddWithValue("@PriceOffered", priceOffered);
                cmd.ExecuteNonQuery();
            }
        }

        [WebMethod]
        public void UpdateQuotationStatus(int quotationId, string status)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Quotations SET status = @Status WHERE id = @QuotationId", conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@QuotationId", quotationId);
                cmd.ExecuteNonQuery();
            }
        }

        [WebMethod]
        public DataTable GetQuotationById(int quotationId)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Quotation";
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Quotations WHERE id = @QuotationId", conn);
                da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                da.Fill(dt);
            }
            return dt;
        }

        [WebMethod]
        public DataTable GetAllQuotations()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Quotations";
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Quotations", conn);
                da.Fill(dt);
            }
            return dt;
        }

        [WebMethod]
        public DataTable GetQuotationsByStatus(string status)
        {
            DataTable dt = new DataTable();
            dt.TableName = "QuotationsByStatus";
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Quotations WHERE status = @Status", conn);
                da.SelectCommand.Parameters.AddWithValue("@Status", status);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
