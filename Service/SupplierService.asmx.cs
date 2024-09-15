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
    /// Summary description for SupplierService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SupplierService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public void AddSupplier(string name, string contactEmail, string contactPhone, string address, string websiteUrl)
        {
            using (SqlConnection connection = Database.GetConnection())
            {
                string query = "INSERT INTO Suppliers (name, contact_email, contact_phone, address, website_url) VALUES (@name, @contactEmail, @contactPhone, @address, @websiteUrl)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@contactEmail", contactEmail);
                command.Parameters.AddWithValue("@contactPhone", contactPhone);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@websiteUrl", websiteUrl);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [WebMethod]
        public void UpdateSupplier(int supplierId, string name, string contactEmail, string contactPhone, string address, string websiteUrl)
        {
            using (SqlConnection connection = Database.GetConnection())
            {
                string query = "UPDATE Suppliers SET name = @name, contact_email = @contactEmail, contact_phone = @contactPhone, address = @address, website_url = @websiteUrl WHERE id = @supplierId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@supplierId", supplierId);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@contactEmail", contactEmail);
                command.Parameters.AddWithValue("@contactPhone", contactPhone);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@websiteUrl", websiteUrl);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [WebMethod]
        public void DeleteSupplier(int supplierId)
        {
            using (SqlConnection connection = Database.GetConnection())
            {
                string query = "DELETE FROM Suppliers WHERE id = @supplierId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@supplierId", supplierId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [WebMethod]
        public Supplier GetSupplierById(int supplierId)
        {
            Supplier supplier = null;
            using (SqlConnection connection = Database.GetConnection())
            {
                string query = "SELECT * FROM Suppliers WHERE id = @supplierId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@supplierId", supplierId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    supplier = new Supplier
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString(),
                        ContactEmail = reader["contact_email"].ToString(),
                        ContactPhone = reader["contact_phone"].ToString(),
                        Address = reader["address"].ToString(),
                        WebsiteUrl = reader["website_url"].ToString()
                    };
                }
            }
            return supplier;
        }

        [WebMethod]
        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (SqlConnection connection = Database.GetConnection())
            {
                string query = "SELECT * FROM Suppliers";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Supplier supplier = new Supplier
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString(),
                        ContactEmail = reader["contact_email"].ToString(),
                        ContactPhone = reader["contact_phone"].ToString(),
                        Address = reader["address"].ToString(),
                        WebsiteUrl = reader["website_url"].ToString()
                    };
                    suppliers.Add(supplier);
                }
            }
            return suppliers;
        }
    }
}
public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }
    public string Address { get; set; }
    public string WebsiteUrl { get; set; }
}