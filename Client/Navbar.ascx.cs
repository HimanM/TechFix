using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Navbar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                litUsername.Text = Session["Username"].ToString(); // Display the logged-in user's name
            }
            else
            {
                Response.Redirect("Login.aspx"); // If no user is logged in, redirect to the login page
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();   // Clear all session data
            Session.Abandon(); // Abandon the session
            Response.Redirect("Login.aspx"); // Redirect to the login page after logout
        }
    }
}