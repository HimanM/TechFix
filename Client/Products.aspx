<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Client.Products" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Products Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

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
        <div>
            <h2>Product Management</h2>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <br /><br />

            <div class="container mt-4 rounded border mb-4 p-3">
                <h2 class="mb-4">Product List</h2>
                <asp:GridView ID="ProductsGrid" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnRowCommand="ProductsGrid_RowCommand" DataKeyNames="id" CssClass="table table-striped" OnPageIndexChanging="ProductsGrid_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="name" HeaderText="Name" />
                        <asp:BoundField DataField="description" HeaderText="Description" />
                        <asp:BoundField DataField="price" HeaderText="Price" />
                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                        <asp:BoundField DataField="category" HeaderText="Category" />
                        <asp:TemplateField HeaderText="Delete Order">
                            <ItemTemplate>
                                 <asp:Button ID="btnDeleteOrder" runat="server" Text="Delete Product" CommandName="Delete_product" CssClass="btn btn-danger inline-element" CommandArgument='<%# Eval("ID") %>' />
                             </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            
                <asp:Button ID="btnLoadProducts" Text="Load Products" runat="server" OnClick="btnLoadProducts_Click" CssClass="btn btn-primary" />
            </div>

            <!-- Add Product -->
            <div class="container mt-4 rounded border mb-4 p-3">
                <h3 class="mb-4">Add Product</h3>
    
                <div class="mb-3">
                    <label for="txtName" class="form-label">Name:</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"/>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="Name is required" CssClass="text-danger"
                                    ValidationGroup="ProductValidation" Display="Dynamic" />
                </div>
    
                <div class="mb-3">
                    <label for="txtDescription" class="form-label">Description:</label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Enter product description"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
                                    ErrorMessage="Description is required" CssClass="text-danger"
                                    ValidationGroup="ProductValidation" Display="Dynamic" />
                </div>
    
                <div class="mb-3">
                    <label for="txtPrice" class="form-label">Price:</label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"  placeholder="Enter product price"/>   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrice"
                    ErrorMessage="Price is required" CssClass="text-danger"
                    ValidationGroup="ProductValidation" Display="Dynamic" />
                </div>
    
                <div class="mb-3">
                    <label for="ddlSuppliers" class="form-label">Supplier:</label>
                    <asp:DropDownList ID="ddlSuppliers" runat="server" AutoPostBack="False" CssClass="form-select" DataTextField="name" DataValueField="id" />
                </div>
    
                <div class="mb-3">
                    <label for="txtCategory" class="form-label">Category:</label>
                    <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" placeholder="Enter product category"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCategory"
                    ErrorMessage="Category is required" CssClass="text-danger"
                    ValidationGroup="ProductValidation" Display="Dynamic" />
                </div>
    
                <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-primary" Text="Add Product" ValidationGroup="ProductValidation" OnClick="btnAddProduct_Click" />
            </div>
            <br /><br />

            <!-- Retrieve Product Details -->
            <div class="container mt-4 rounded border mb-4 p-3">
                <h3 class="mb-4">Product Details</h3>
                <div class="mb-3">
                    <asp:Label ID="lblProductId" runat="server" Text="Product ID: " />
                    <asp:TextBox ID="txtProductId" runat="server" CssClass="form-control" placeholder="Enter product ID"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtProductId"
                    ErrorMessage="Product ID is required" CssClass="text-danger"
                    ValidationGroup="ProductDetailValidation" Display="Dynamic" />
                </div>
                <br />
                <asp:Button ID="btnGetProduct" runat="server" Text="Get Product" OnClick="btnGetProduct_Click" CssClass="btn btn-primary" ValidationGroup="ProductDetailValidation"/>
                <asp:GridView ID="gvProductDetails" runat="server" AutoGenerateColumns="true" CssClass="table table-striped"></asp:GridView>
            </div>
        </div>
    </form>

</body>
</html>
