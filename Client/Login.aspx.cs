using Client.LoginService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Login : System.Web.UI.Page
    {
        LoginService.LoginServiceSoapClient loginService = new LoginService.LoginServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["Username"] != null)
            {
                Response.Redirect("Home.aspx"); // If already logged in, redirect to home
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // You can replace this logic with actual database authentication
            if (AuthenticateUser(username, password))
            {
                // Store username in session
                Session["Username"] = username;
                Response.Redirect("Home.aspx"); // Redirect to home page after successful login
            }
            else
            {
                lblError.Visible = true; // Show error message
            }
        }

        // Authentication method (replace with database logic)
        private bool AuthenticateUser(string username, string password)
        {
            // Example: Assume correct credentials are "admin" and "password"
            return (loginService.ValidateUser(username, password));
        }
    }
}