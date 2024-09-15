using Client.SupplierService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Quotation : System.Web.UI.Page
    {
        QuotationService.QuotationServiceSoapClient quotationService = new QuotationService.QuotationServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllQuotations();
                LoadStatus();
                LoadSuppliers();
                LoadProducts();
            }
        }

        private void LoadAllQuotations()
        {
            DataTable dt = quotationService.GetAllQuotations();
            QuotationsGrid.DataSource = dt;
            QuotationsGrid.DataBind();
        }

        protected void btnAddQuotation_Click(object sender, EventArgs e)
        {

            int supplierId = int.Parse(ddlSuppliers.SelectedValue);
            int productId = int.Parse(ddlProducts.SelectedValue);
            int quantityRequested = int.Parse(txtQuantityRequested.Text);
            decimal priceOffered = decimal.Parse(txtPriceOffered.Text);
            if(quantityRequested > 0 || priceOffered > 0)
            {
                quotationService.AddQuotation(supplierId, productId, quantityRequested, priceOffered);
                LoadAllQuotations();
            }
            else
            {
                lblMessage.Text = "Enter Valid Price/Quantity";
            }

            
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {

            int quotationId = int.Parse(txtQuotationId.Text);
            string status = ddlStatus.SelectedValue;

            quotationService.UpdateQuotationStatus(quotationId, status);
            LoadAllQuotations();
        }

        protected void btnGetQuotation_Click(object sender, EventArgs e)
        {
            

            int quotationId = int.Parse(txtQuotationId.Text);
            DataTable dt = quotationService.GetQuotationById(quotationId);
            PopulateFields(dt);
            QuotationsGrid.DataSource = dt;
            QuotationsGrid.DataBind();
        }

        private void PopulateFields(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                SupplierService.SupplierServiceSoapClient supplierClient = new SupplierService.SupplierServiceSoapClient();
                ProductService.ProductServiceSoapClient productClient = new ProductService.ProductServiceSoapClient();
                // Assuming you want to populate the first row
                DataRow row = dt.Rows[0];

                // Populate TextBox fields
                txtQuotationId.Text = row["id"].ToString();
                DataSet product_ds= productClient.GetProductById(int.Parse(row["product_id"].ToString()));
                DataTable product_dt = product_ds.Tables[0];
                var product = product_dt.Rows[0];
                var supplier = supplierClient.GetSupplierById(int.Parse(row["supplier_id"].ToString()));
                
                if (supplier != null)
                {
                    ddlSuppliers.Items.Clear();
                    ddlSuppliers.Items.Add(new ListItem(supplier.Name, row["supplier_id"].ToString()));
                }
                if (product != null)
                {
                    ddlProducts.Items.Clear();
                    ddlProducts.Items.Add(new ListItem(product["name"].ToString(), row["product_id"].ToString()));
                }
                txtQuantityRequested.Text = row["quantity_requested"].ToString();
                txtPriceOffered.Text = row["price_offered"].ToString();

                // Populate DropDownList field
                // Note: DropDownList should be populated with data first
                ddlStatus.SelectedValue = row["status"].ToString();
            }
        }

        protected void btnGetAllQuotations_Click(object sender, EventArgs e)
        {
            LoadAllQuotations();
        }

        protected void btnGetQuotationsByStatus_Click(object sender, EventArgs e)
        {
            
            string status = ddlStatus.SelectedValue;
            DataTable dt = quotationService.GetQuotationsByStatus(status);
            QuotationsGrid.DataSource = dt;
            QuotationsGrid.DataBind();
        }

        private void LoadStatus()
        {
            
            ddlStatus.Items.Add(new ListItem("Pending", "Pending"));
            ddlStatus.Items.Add(new ListItem("Accepted", "Accepted"));
            ddlStatus.Items.Add(new ListItem("Declined", "Declined"));
        }

        private void LoadSuppliers()
        {
            try
            {
                SupplierService.SupplierServiceSoapClient client = new SupplierService.SupplierServiceSoapClient();
                // Call GetAllSuppliers from the SupplierService to populate the dropdown
                var suppliers = client.GetAllSuppliers();
                
                if (suppliers != null)
                {

                    ddlSuppliers.DataTextField = "Name";
                    ddlSuppliers.DataValueField = "Id";
                    ddlSuppliers.DataSource = suppliers;
                    ddlSuppliers.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                Response.Write("Error loading suppliers: " + ex.Message);
            }
        }

        private void LoadProducts()
        {
            try
            {
                ProductService.ProductServiceSoapClient client = new ProductService.ProductServiceSoapClient();
                // Call GetAllSuppliers from the SupplierService to populate the dropdown
                var products = client.GetAllProducts();

                ddlSuppliers.Items.Add(new ListItem("Select Product", ""));
                if (products != null)
                {

                    ddlProducts.DataTextField = "name";
                    ddlProducts.DataValueField = "id";
                    ddlProducts.DataSource = products;
                    ddlProducts.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                Response.Write("Error loading suppliers: " + ex.Message);
            }
        }

        protected void btnClearFields_Click(object sender, EventArgs e)
        {
            // Clear all text fields
            txtQuotationId.Text = string.Empty;
            txtQuantityRequested.Text = string.Empty;
            txtPriceOffered.Text = string.Empty;

            // Reset dropdowns to the default option
            LoadSuppliers();
            LoadStatus();
            LoadProducts();
        }
    }
}