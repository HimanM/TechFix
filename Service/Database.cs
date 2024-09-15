using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

public class Database
{
    private static string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TechFix;Integrated Security=True;Encrypt=False;";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}
