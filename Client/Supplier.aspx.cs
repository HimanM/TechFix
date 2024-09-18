using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Client
{
    public partial class Supplier : System.Web.UI.Page
    {
        SupplierService.SupplierServiceSoapClient supplierService = new SupplierService.SupplierServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx"); // Redirect to login page if not logged in
                }
                LoadSuppliers();
            }
        }

        protected void SupplierGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            SupplierGrid.PageIndex = e.NewPageIndex;

            // Rebind the GridView to the data source
            SupplierGrid.DataBind();
            LoadSuppliers();
        }
        private void LoadSuppliers()
        {
            var suppliers = supplierService.GetAllSuppliers();
            if (suppliers != null)
            {
                ddlSuppliers.DataSource = suppliers;
                ddlSuppliers.DataTextField = "Name";
                ddlSuppliers.DataValueField = "Id";
                ddlSuppliers.DataBind();
                ddlSuppliers.Items.Add(new ListItem("Select Supplier", ""));
                ddlSuppliers.SelectedIndex = ddlSuppliers.Items.Count - 1;

                SupplierGrid.DataSource = suppliers;
                SupplierGrid.DataBind();

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string web = txtWebsite.Text;
            String phone = txtPhone.Text;

            if (IsValidEmail(email) && IsValidUrl(web) && IsValidPhoneNumber(phone))
            {
                supplierService.AddSupplier(txtName.Text, txtEmail.Text, txtPhone.Text, txtAddress.Text, txtWebsite.Text);
                lblMessage.Text = "Supplier added successfully!";
                LoadSuppliers();
            }
            else
            {
                lblMessage.Text = "Check Your Email/ Website/ Phone Format..!";
            }
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
           
            if(!(ddlSuppliers.SelectedValue == ""))
            {
                int supplierId = int.Parse(ddlSuppliers.SelectedValue);
                supplierService.UpdateSupplier(supplierId, txtName.Text, txtEmail.Text, txtPhone.Text, txtAddress.Text, txtWebsite.Text);
                lblMessage.Text = "Supplier updated successfully!";
                LoadSuppliers();
            }
            else
            {
                lblMessage.Text = "Select The Supplier";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!(ddlSuppliers.SelectedValue == "")) 
            { 
                int supplierId = int.Parse(ddlSuppliers.SelectedValue);
                supplierService.DeleteSupplier(supplierId);
                lblMessage.Text = "Supplier deleted successfully!";
                LoadSuppliers();
            }
            else
            {
                lblMessage.Text = "Select The Supplier";
            }
        }

        protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(ddlSuppliers.SelectedValue == ""))
            {
                int supplierId = int.Parse(ddlSuppliers.SelectedValue);
                var supplier = supplierService.GetSupplierById(supplierId);
                if (supplier != null)
                {
                    txtName.Text = supplier.Name;
                    txtEmail.Text = supplier.ContactEmail;
                    txtPhone.Text = supplier.ContactPhone;
                    txtAddress.Text = supplier.Address;
                    txtWebsite.Text = supplier.WebsiteUrl;
                }
            }
            else
            {
                txtName.Text = String.Empty;
                txtEmail.Text = String.Empty;
                txtPhone.Text = String.Empty;
                txtAddress.Text = String.Empty;
                txtWebsite.Text = String.Empty;
            }
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public bool IsValidUrl(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
            {
                return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            }
            return false;
        }

        private static readonly string PhoneNumberPattern = @"^\+?\d{1,4}[-.\s]?(?:\d{1,4}[-.\s]?){1,4}\d{1,4}$";

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(PhoneNumberPattern);
            return regex.IsMatch(phoneNumber);
        }
    }
}