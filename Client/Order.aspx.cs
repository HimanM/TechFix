using Client.OrderService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Order : System.Web.UI.Page
    {
        private OrderService.OrderServiceSoapClient orderService = new OrderService.OrderServiceSoapClient();
        private ProductService.ProductServiceSoapClient productService = new ProductService.ProductServiceSoapClient();
        private SupplierService.SupplierServiceSoapClient supplierService = new SupplierService.SupplierServiceSoapClient();

        

        private List<OrderItem> temporaryOrderItems
        {
            get
            {
                if (Session["TemporaryOrderItems"] == null)
                {
                    Session["TemporaryOrderItems"] = new List<OrderItem>();
                }
                return (List<OrderItem>)Session["TemporaryOrderItems"];
            }
            set
            {
                Session["TemporaryOrderItems"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx"); // Redirect to login page if not logged in
                }
                LoadSuppliers();
                LoadOrders();
                
            }
        }
        private void LoadOrders()
        {
            var orders = orderService.GetAllOrders();
            if (orders != null)
            {
                gvOrders.DataSource = orders;
                gvOrders.DataBind();
            }
        }
        private void LoadSuppliers()
        {
            var suppliers = supplierService.GetAllSuppliers();

            
            if (suppliers != null)
            {
                ddlSuppliers.DataTextField = "name";
                ddlSuppliers.DataValueField = "id";
                ddlSuppliers.DataSource = suppliers;
                ddlSuppliers.DataBind();
                ddlSuppliers.Items.Add(new ListItem("Select Supplier", ""));
                ddlSuppliers.SelectedIndex = ddlSuppliers.Items.Count - 1;
            }
        }

        protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(ddlSuppliers.SelectedValue == "")) {
                int supplierId = Convert.ToInt32(ddlSuppliers.SelectedValue);
                LoadProducts(supplierId); 
            }
            else
            {
                ClearGridView();
            }
        }

        private void LoadProducts(int supplierId)
        {
            var products = productService.GetProductBySupplierId(supplierId);
            if (products != null)
            {
                gvProducts.DataSource = products;
                gvProducts.DataBind();
            }
        }

        private void ClearGridView()
        {
            gvProducts.DataSource = null;
            gvProducts.DataBind();
        }
        protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            gvProducts.PageIndex = e.NewPageIndex;
            int supplierId = Convert.ToInt32(ddlSuppliers.SelectedValue);

            // Rebind the GridView to the data source
            gvProducts.DataBind();
            LoadProducts(supplierId);
        }
        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add_product")
            {
                
                int productId = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");

                // Validate quantity input
                if (!int.TryParse(txtQuantity.Text, out int quantityOrdered) || quantityOrdered <= 0)
                {
                    lblMessage.Text = "Please select a valid quantity";
                    return;
                }

                // Get product details from the service
                DataSet productDataSet = productService.GetProductById(productId);
                if (productDataSet != null && productDataSet.Tables.Count > 0)
                {
                    


                    DataTable productTable = productDataSet.Tables[0];
                    if (productTable.Rows.Count > 0)
                    {
                        DataRow productRow = productTable.Rows[0];
                        decimal pricePerUnit = Convert.ToDecimal(productRow["price"]);
                        string productName = productRow["name"].ToString();

                        // Create and add the order item to the temporary list
                        OrderItem item = new OrderItem
                        {
                            ProductId = productId,
                            ProductName = productName,
                            QuantityOrdered = quantityOrdered,
                            PricePerUnit = pricePerUnit
                        };

                        // Add to temporary order items list
                        var items = temporaryOrderItems;
                        items.Add(item);
                        temporaryOrderItems = items;

                        BindOrderItemsGrid(); // Refresh the GridView with the updated items
                    }
                }
            }
            
        }

        private void BindOrderItemsGrid()
        {
            gvOrderItems.DataSource = temporaryOrderItems;
            gvOrderItems.DataBind();
        }

        protected void gvOrderItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                if (index >= 0 && index < temporaryOrderItems.Count)
                {
                    temporaryOrderItems.RemoveAt(index);
                    BindOrderItemsGrid();
                }
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if(!(ddlSuppliers.SelectedValue == ""))
            {
                int supplierId = Convert.ToInt32(ddlSuppliers.SelectedValue);

                // Calculate the total amount from the temporary order items
                decimal totalAmount = temporaryOrderItems.Sum(item => item.QuantityOrdered * item.PricePerUnit);

                // Call AddOrder with collected data
                orderService.AddOrder(supplierId, totalAmount, temporaryOrderItems.ToArray());


                temporaryOrderItems.Clear(); // Clear temporary list
                BindOrderItemsGrid(); // Clear the grid
                LoadOrders(); // Reload orders
            }
            
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewItems")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                var orderItems = orderService.GetOrderItems(orderId);
                if (orderItems != null)
                {
                    gvOrderItemsList.DataSource = orderItems;
                    gvOrderItemsList.DataBind();
                    pnlOrderItems.Visible = true; // Show panel with order items
                }
            }
            if (e.CommandName == "Delete_product")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                orderService.DeleteOrder(orderId);
                BindOrderItemsGrid();
                lblMessage.Text = "Order Deleted successfully!";
                LoadOrders();
            }
            if (e.CommandName == "UpdateStatus")
            {
                // Get the row index
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Find the DropDownList in the selected row
                GridViewRow row = gvOrders.Rows[rowIndex];
                DropDownList ddlOrderStatus = row.FindControl("ddlOrderStatus") as DropDownList;

                // Check if DropDownList was found and get the selected value
                if (ddlOrderStatus != null)
                {
                    string newStatus = ddlOrderStatus.SelectedValue;

                    if (!string.IsNullOrEmpty(newStatus))
                    {
                        // Assuming OrderId is bound to the GridView, you can get it like this
                        int orderId = Convert.ToInt32(gvOrders.DataKeys[rowIndex].Value);

                        // Call your service to update the order status
                        orderService.UpdateOrderStatus(orderId, newStatus);

                        lblMessage.Text = "Order status updated successfully!";
                        LoadOrders(); // Reload the grid after the update
                    }
                    else
                    {
                        lblMessage.Text = "Please select a valid status.";
                    }
                }
                else
                {
                    lblMessage.Text = "Failed to find the status dropdown.";
                }
            }
                

        }

    }
}