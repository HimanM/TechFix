<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="Client.Quotation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quotation Management</title>
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
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <div class ="row justify-content-center">
            <!-- Section for Managing Quotations -->
            <div class="col-4">
                <div class="container mt-4 rounded border mb-4 p-3">
                    <h2 class="mb-4">Manage Quotations</h2>

                    <asp:Button ID="btnAddQuotation" runat="server" Text="Add Quotation" CssClass="btn btn-primary" OnClick="btnAddQuotation_Click" ValidationGroup="AddUpdate" />
                    <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" CssClass="btn btn-warning" OnClick="btnUpdateStatus_Click" ValidationGroup="AddUpdate" />
                    <asp:Button ID="btnGetQuotation" runat="server" Text="Get Quotation" CssClass="btn btn-info" OnClick="btnGetQuotation_Click" ValidationGroup="GetQuotation" />
                    <asp:Button ID="btnGetAllQuotations" runat="server" Text="Get All Quotations" CssClass="btn btn-secondary" OnClick="btnGetAllQuotations_Click" />
                    <asp:Button ID="btnGetQuotationsByStatus" runat="server" Text="Get Quotations by Status" CssClass="btn btn-success" OnClick="btnGetQuotationsByStatus_Click" ValidationGroup="GetByStatus" />

                    <div class="mb-3 mt-4 p-3">
                        <asp:Label ID="lblQuotationId" runat="server" Text="Quotation ID: " />
                        <asp:TextBox ID="txtQuotationId" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvQuotationId" runat="server" ControlToValidate="txtQuotationId" 
                                                    ErrorMessage="Quotation ID is required" CssClass="text-danger" 
                                                    Display="Dynamic" ValidationGroup="GetQuotation" />
                    </div>

                    <%--Suppliers--%>
                    <div class="mb-3 mt-4 p-3">
                        <label for="ddlSuppliers" class="form-label">Supplier:</label>
                        <asp:DropDownList ID="ddlSuppliers" runat="server" AutoPostBack="False" CssClass="form-select" DataTextField="name" DataValueField="id">  
                            <asp:ListItem Text="Select Supplier" Value="" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSupplierValue" runat="server" ControlToValidate="ddlSuppliers" 
                             InitialValue="" ErrorMessage="Supplier is required" CssClass="text-danger" 
                             Display="Dynamic" ValidationGroup="AddUpdate" />
                    </div>

                    <%--Products--%>

                    <div class="mb-3 mt-4 p-3">
                        <label for="ddlProducts" class="form-label">Product:</label>
                        <asp:DropDownList ID="ddlProducts" runat="server" AutoPostBack="False" CssClass="form-select" DataTextField="name" DataValueField="id"> 
                            <asp:ListItem Text="Select Product" Value="" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProducts" 
                             InitialValue="" ErrorMessage="Produsct is required" CssClass="text-danger" 
                             Display="Dynamic" ValidationGroup="AddUpdate" />
                    </div>


                    <div class="mb-3 p-3">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity Requested: " />
                        <asp:TextBox ID="txtQuantityRequested" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvQuantityRequested" runat="server" ControlToValidate="txtQuantityRequested" 
                                                    ErrorMessage="Quantity Requested is required" CssClass="text-danger" 
                                                    Display="Dynamic" ValidationGroup="AddUpdate" />
                    </div>

                    <div class="mb-3 p-3">
                        <asp:Label ID="lblPrice" runat="server" Text="Price Offered: " />
                        <asp:TextBox ID="txtPriceOffered" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvPriceOffered" runat="server" ControlToValidate="txtPriceOffered" 
                                                    ErrorMessage="Price Offered is required" CssClass="text-danger" 
                                                    Display="Dynamic" ValidationGroup="AddUpdate" />
                    </div>

                    <div class="mb-3 p-3">
                        <asp:Label ID="lblStatus" runat="server" Text="Status: " />
                        <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="False" CssClass="form-select" DataTextField="name" DataValueField="id" />
                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" 
                                                    InitialValue="0" ErrorMessage="Status is required" CssClass="text-danger" 
                                                    Display="Dynamic" ValidationGroup="GetByStatus" />
                    </div>
                    <asp:Button ID="btnClearFields" runat="server" Text="Clear Fields" CssClass="btn btn-danger" OnClick="btnClearFields_Click" />
                </div>
            </div>

            <div class="col-7">
                <div class="container mt-4 rounded border mb-4 p-3">
                    <asp:GridView ID="QuotationsGrid" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="supplier_id" HeaderText="Supplier ID" />
                            <asp:BoundField DataField="product_id" HeaderText="Product ID" />
                            <asp:BoundField DataField="quantity_requested" HeaderText="Quantity Requested" />
                            <asp:BoundField DataField="price_offered" HeaderText="Price Offered" />
                            <asp:BoundField DataField="status" HeaderText="Status" />
                            <asp:BoundField DataField="created_at" HeaderText="Created At" />
                            <asp:BoundField DataField="updated_at" HeaderText="Updated At" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.5/jquery.validate.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js"></script>
</body>
</html>
