<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="Client.Inventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</head>
    <body>

        <nav class="navbar navbar-expand-lg navbar-light bg-light">
          <div class="container-fluid">
            <a class="navbar-brand" href="#">TechFix SOP</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
              <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
              <ul class="navbar-nav">
                <li class="nav-item">
                  <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Home.aspx" CssClass="nav-link">Home</asp:HyperLink>
                </li>
                <li class="nav-item">
                  <asp:HyperLink ID="lnkProducts" runat="server" NavigateUrl="~/Products.aspx" CssClass="nav-link">Products</asp:HyperLink>
                </li>
                <li class="nav-item">
                  <asp:HyperLink ID="lnkSuppliers" runat="server" NavigateUrl="~/Supplier.aspx" CssClass="nav-link">Suppliers</asp:HyperLink>
                </li>
                <li class="nav-item">
                  <asp:HyperLink ID="lnkInventory" runat="server" NavigateUrl="~/Inventory.aspx" CssClass="nav-link">Inventory</asp:HyperLink>
                </li>
                <li class="nav-item">
                  <asp:HyperLink ID="lnkOrders" runat="server" NavigateUrl="~/Order.aspx" CssClass="nav-link">Orders</asp:HyperLink>
                </li>
                <li class="nav-item">
                  <asp:HyperLink ID="lnkQuotation" runat="server" NavigateUrl="~/Quotation.aspx" CssClass="nav-link">Quotations</asp:HyperLink>
                </li>
              </ul>
            </div>
          </div>
        </nav>


    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2>Inventory Management</h2>
            <br />
            <h3>Inventory List</h3>
            <asp:GridView ID="InventoryGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                    <asp:BoundField DataField="quantity_available" HeaderText="Quantity Available" />
                    <asp:BoundField DataField="last_updated" HeaderText="Last Updated" DataFormatString="{0:dd/MM/yyyy}" />
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <!-- Section to Add a New Product to Inventory -->
            <div class="container mt-4 rounded border mb-4 p-3">
                <h3 class="mb-4">Add New Product to Inventory</h3>
                <div class="mb-1">
                    <label for="ddlProducts" class="form-label">Product:</label>
                    <asp:DropDownList ID="ddlProducts" runat="server" CssClass="form-select" />
                    <asp:RequiredFieldValidator ID="rfvProduct" runat="server" ControlToValidate="ddlProducts" 
                        InitialValue="-1" ErrorMessage="Please select a product." CssClass="text-danger" 
                        ValidationGroup="AddInventoryGroup" />
                </div>

                <div class="mb-1">
                    <label for="ddlSuppliers" class="form-label">Supplier:</label>
                    <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-select" />
                    <asp:RequiredFieldValidator ID="rfvSupplier" runat="server" ControlToValidate="ddlSuppliers" 
                        InitialValue="-1" ErrorMessage="Please select a supplier." CssClass="text-danger" 
                        ValidationGroup="AddInventoryGroup" />
                </div>

                <div class="mb-1">
                    <label for="txtQuantityAvailable" class="form-label">Quantity Available:</label>
                    <asp:TextBox ID="txtQuantityAvailable" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvQuantityAvailable" runat="server" ControlToValidate="txtQuantityAvailable" 
                        ErrorMessage="Quantity is required." CssClass="text-danger" 
                        ValidationGroup="AddInventoryGroup" />
                    <asp:RangeValidator ID="rvQuantityAvailable" runat="server" ControlToValidate="txtQuantityAvailable" 
                        MinimumValue="1" MaximumValue="10000" Type="Integer" ErrorMessage="Enter a valid quantity." 
                        CssClass="text-danger" ValidationGroup="AddInventoryGroup" />
                </div>

                <asp:Button ID="btnAddProductToInventory" runat="server" Text="Add to Inventory" CssClass="btn btn-primary" 
                    OnClick="btnAddProductToInventory_Click" ValidationGroup="AddInventoryGroup" />
            </div>

            <!-- Section to Edit Inventory -->
            <div class="container mt-4 rounded border mb-4 p-3">
                <h3 class="mb-4">Edit Product Quantity</h3>
                <div class="mb-1">
                    <label for="ddlEditProducts" class="form-label">Product:</label>
                    <asp:DropDownList ID="ddlEditProducts" runat="server" CssClass="form-select" />
                    <asp:RequiredFieldValidator ID="rfvEditProduct" runat="server" ControlToValidate="ddlEditProducts" 
                        InitialValue="-1" ErrorMessage="Please select a product." CssClass="text-danger" 
                        ValidationGroup="EditInventoryGroup" />
                </div>

                <div class="mb-1">
                    <label for="txtNewQuantity" class="form-label">New Quantity:</label>
                    <asp:TextBox ID="txtNewQuantity" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvNewQuantity" runat="server" ControlToValidate="txtNewQuantity" 
                        ErrorMessage="New quantity is required." CssClass="text-danger" 
                        ValidationGroup="EditInventoryGroup" />
                    <asp:RangeValidator ID="rvNewQuantity" runat="server" ControlToValidate="txtNewQuantity" 
                        MinimumValue="1" MaximumValue="10000" Type="Integer" ErrorMessage="Enter a valid quantity." 
                        CssClass="text-danger" ValidationGroup="EditInventoryGroup" />
                </div>

                <asp:Button ID="btnEditInventory" runat="server" Text="Update Inventory" CssClass="btn btn-warning" 
                    OnClick="btnEditInventory_Click" ValidationGroup="EditInventoryGroup" />
            </div>

            <!-- Section to Remove Quantity from Inventory -->
            <div class="container mt-4 rounded border mb-4 p-3">
                <h3 class="mb-4">Remove Quantity from Inventory</h3>
                <div class="mb-1">
                    <label for="ddlRemoveProducts" class="form-label">Product:</label>
                    <asp:DropDownList ID="ddlRemoveProducts" runat="server" CssClass="form-select" />
                    <asp:RequiredFieldValidator ID="rfvRemoveProduct" runat="server" ControlToValidate="ddlRemoveProducts" 
                        InitialValue="-1" ErrorMessage="Please select a product." CssClass="text-danger" 
                        ValidationGroup="RemoveInventoryGroup" />
                </div>

                <div class="mb-1">
                    <label for="txtQuantityToRemove" class="form-label">Quantity to Remove:</label>
                    <asp:TextBox ID="txtQuantityToRemove" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvQuantityToRemove" runat="server" ControlToValidate="txtQuantityToRemove" 
                        ErrorMessage="Quantity to remove is required." CssClass="text-danger" 
                        ValidationGroup="RemoveInventoryGroup" />
                    <asp:RangeValidator ID="rvQuantityToRemove" runat="server" ControlToValidate="txtQuantityToRemove" 
                        MinimumValue="1" MaximumValue="10000" Type="Integer" ErrorMessage="Enter a valid quantity." 
                        CssClass="text-danger" ValidationGroup="RemoveInventoryGroup" />
                </div>

                <asp:Button ID="btnRemoveQuantity" runat="server" Text="Remove from Inventory" CssClass="btn btn-danger" 
                    OnClick="btnRemoveQuantity_Click" ValidationGroup="RemoveInventoryGroup" />
            </div>

            <!-- Section to View Inventory -->
            <div class="container mt-4 rounded border mb-4 p-3">
                <h3 class="mb-4"> View Inventory</h3>
                <div class="mb-1">
                    <label for="ddlViewProducts" class="form-label">Product:</label>
                    <asp:DropDownList ID="ddlViewProducts" runat="server" CssClass="form-select"/>
                    <br />

                    <asp:Button ID="btnViewInventory" runat="server" Text="View Inventory" OnClick="btnViewInventory_Click" CssClass="btn btn-primary" />
                    <br />
                </div>
                <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="true" CssClass="table table-striped" />
            </div>
        </div>
    </form>
</body>
</html>
