using Client.InventoryService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Inventory : System.Web.UI.Page
    {
        InventoryServiceSoapClient inventoryService = new InventoryServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProductDropDowns();
                LoadSupplierDropDown();
                LoadInventory();
            }
        }

        private void LoadInventory()
        {
            InventoryService.InventoryServiceSoapClient inventoryService = new InventoryService.InventoryServiceSoapClient();
            DataSet ds = inventoryService.GetAllInventory();
            if (ds.Tables[0].Rows.Count > 0)
            {
                InventoryGrid.DataSource = ds;
                InventoryGrid.DataBind();
            }
            else
            {
                //lblMessage.Text = "No product found with this ID.";
            }
        }
        private void LoadProductDropDowns()
        {
            // Assuming you have a ProductService to fetch the products
            ProductService.ProductServiceSoapClient productService = new ProductService.ProductServiceSoapClient();
            InventoryService.InventoryServiceSoapClient inventoryService = new InventoryService.InventoryServiceSoapClient();
            DataSet products = productService.GetAllProducts();
            DataSet productsInv = inventoryService.GetAllInventoryProducts();

            if (products != null && products.Tables.Count > 0)
            {
                ddlProducts.DataSource = products;
                ddlProducts.DataTextField = "name";
                ddlProducts.DataValueField = "id";
                ddlProducts.DataBind();

                ddlEditProducts.DataSource = productsInv;
                ddlEditProducts.DataTextField = "ProductName";
                ddlEditProducts.DataValueField = "product_id";
                ddlEditProducts.DataBind();

                ddlRemoveProducts.DataSource = productsInv;
                ddlRemoveProducts.DataTextField = "ProductName";
                ddlRemoveProducts.DataValueField = "product_id";
                ddlRemoveProducts.DataBind();

                ddlViewProducts.DataSource = productsInv;
                ddlViewProducts.DataTextField = "ProductName";
                ddlViewProducts.DataValueField = "product_id";
                ddlViewProducts.DataBind();
            }
        }

        private void LoadSupplierDropDown()
        {
            // Assuming you have a SupplierService to fetch the suppliers
            SupplierService.SupplierServiceSoapClient supplierService = new SupplierService.SupplierServiceSoapClient();
            SupplierService.Supplier[] suppliers = supplierService.GetAllSuppliers();  // Get the array of suppliers

            if (suppliers != null && suppliers.Length > 0)
            {
                ddlSuppliers.DataSource = suppliers; // Directly bind the array
                ddlSuppliers.DataTextField = "Name"; // Matching the property in the Supplier object
                ddlSuppliers.DataValueField = "Id";  // Matching the property in the Supplier object
                ddlSuppliers.DataBind();
            }
        }

        protected void btnAddProductToInventory_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(ddlProducts.SelectedValue);
            int supplierId = Convert.ToInt32(ddlSuppliers.SelectedValue);
            int quantityAvailable = Convert.ToInt32(txtQuantityAvailable.Text);

            string result = inventoryService.AddProductToInventory(productId, supplierId, quantityAvailable);
            Response.Write(result);
            LoadInventory();
        }

        protected void btnEditInventory_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(ddlEditProducts.SelectedValue);
            int newQuantity = Convert.ToInt32(txtNewQuantity.Text);

            string result = inventoryService.EditInventoryQuantity(productId, newQuantity);
            Response.Write(result);
            LoadInventory();
        }

        protected void btnRemoveQuantity_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(ddlRemoveProducts.SelectedValue);
            int quantityToRemove = Convert.ToInt32(txtQuantityToRemove.Text);

            string result = inventoryService.RemoveFromInventory(productId, quantityToRemove);
            Response.Write(result);
            LoadInventory();
        }

        protected void btnViewInventory_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(ddlViewProducts.SelectedValue);
            DataSet ds = inventoryService.GetInventoryByProductId(productId);

            if (ds != null && ds.Tables.Count > 0)
            {
                gvInventory.DataSource = ds;
                gvInventory.DataBind();
            }
            else
            {
                Response.Write("No inventory data found.");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}