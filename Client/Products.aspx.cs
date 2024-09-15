using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllProducts();
                LoadSuppliers();
            }
        }

        protected void ProductsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            ProductsGrid.PageIndex = e.NewPageIndex;

            // Rebind the GridView to the data source
            ProductsGrid.DataBind();
            LoadAllProducts();
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            ProductService.ProductServiceSoapClient client = new ProductService.ProductServiceSoapClient();

            string name = txtName.Text;
            string description = txtDescription.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            int supplierId = int.Parse(ddlSuppliers.SelectedValue);
            string category = txtCategory.Text;
            if (price <= 0)
            {
                lblMessage.Text = "Please Enter a Valid Price";
            }
            else
            {
                try
                {
                    client.AddProduct(name, description, price, supplierId, category);
                    lblMessage.Text = "Product added successfully!";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }

            
        }

        protected void btnGetProduct_Click(object sender, EventArgs e)
        {
            ProductService.ProductServiceSoapClient client = new ProductService.ProductServiceSoapClient();

            int productId = int.Parse(txtProductId.Text);

            try
            {
                DataSet ds = client.GetProductById(productId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvProductDetails.DataSource = ds;
                    gvProductDetails.DataBind();
                }
                else
                {
                    lblMessage.Text = "No product found with this ID.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
        protected void btnLoadProducts_Click(object sender, EventArgs e)
        {
            LoadAllProducts();
        }

        private void LoadAllProducts()
        {
            ProductService.ProductServiceSoapClient client = new ProductService.ProductServiceSoapClient();
            try
            {
                // Call GetAllProducts from the web service
                var products = client.GetAllProducts();

                // Check if products exist and bind to GridView
                if (products != null)
                {
                    ProductsGrid.DataSource = products;
                    ProductsGrid.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                Response.Write("Error: " + ex.Message);
            }
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

        protected void ProductsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete_product")
            {
                ProductService.ProductServiceSoapClient client = new ProductService.ProductServiceSoapClient();
                int productId = Convert.ToInt32(e.CommandArgument);

                // Call the DeleteProduct method from the ProductService
                client.DeleteProduct(productId);

                // Re-load the products to reflect the changes
                LoadAllProducts();
                LoadSuppliers();
                lblMessage.Text = "Product Deleted";
            }
        }
    }
}