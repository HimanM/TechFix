<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="Client.Order" %>
<%@ Register Src="Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
         <!-- Include the Navbar -->
        <uc:Navbar ID="Navbar1" runat="server" />
        <div>
            <div class="container mt-4 rounded border mb-4 p-3">
                <h2>Select Supplier</h2>
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlSuppliers" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged" CssClass="form-select">
                    <asp:ListItem Text="Select Supplier" Value="" />
                </asp:DropDownList>
                <br />

                <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False"  AllowPaging="True" PageSize="10"  OnRowCommand="gvProducts_RowCommand" DataKeyNames="id" CssClass="table table-striped" OnPageIndexChanging="gvProducts_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Product ID" />
                        <asp:BoundField DataField="name" HeaderText="Product Name" />
                        <asp:BoundField DataField="price" HeaderText="Price" />
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Text="0" Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnAddToOrder" runat="server" Text="Add" CommandName="Add_product" CssClass="btn btn-primary" CommandArgument='<%# Eval("id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
            </div>

            <div class="container mt-4 rounded border mb-4 p-3">
                <h2>Order Items</h2>
                <asp:GridView ID="gvOrderItems" runat="server" AutoGenerateColumns="False" OnRowCommand="gvOrderItems_RowCommand" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                        <asp:BoundField DataField="QuantityOrdered" HeaderText="Quantity Ordered" />
                        <asp:BoundField DataField="PricePerUnit" HeaderText="Price Per Unit" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnRemoveItem" runat="server" Text="Remove" CommandName="RemoveItem" CssClass="btn btn-danger" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" OnClick="btnPlaceOrder_Click" CssClass="btn btn-primary"/>
                <br /><br />
            </div>


            <div class="container mt-4 rounded border mb-4 p-3">
                <h2>Order List</h2>
                <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" OnRowCommand="gvOrders_RowCommand" CssClass="table table-striped" enableViewState="true" DataKeyNames="OrderId">
                    <Columns>
                        <asp:BoundField DataField="OrderId" HeaderText="Order ID" />
                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:TemplateField HeaderText="View Order Items">
                            <ItemTemplate>
                                <asp:Button ID="btnViewItems" runat="server" Text="View Items" CommandName="ViewItems" CssClass="btn btn-primary" CommandArgument='<%# Eval("OrderId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete Order">
                             <ItemTemplate>
                                 <asp:Button ID="btnDeleteOrder" runat="server" Text="Delete Order" CommandName="Delete_product" CssClass="btn btn-danger inline-element" CommandArgument='<%# Eval("OrderId") %>' />
                             </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Change Status">
                            <ItemTemplate>
                                <!-- DropDownList for selecting new order status -->
                                <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Edit Status" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                    <asp:ListItem Text="Shipped" Value="Shipped"></asp:ListItem>
                                    <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>
                                    <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                                </asp:DropDownList>
                                <!-- Button to update the status -->
                                <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" CommandName="UpdateStatus" CssClass="btn btn-success inline-element" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <br /><br />
                <asp:Panel ID="pnlOrderItems" runat="server" Visible="False">
                    <h2>Order Items</h2>
                    <asp:GridView ID="gvOrderItemsList" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                            <asp:BoundField DataField="QuantityOrdered" HeaderText="Quantity Ordered" />
                            <asp:BoundField DataField="PricePerUnit" HeaderText="Price Per Unit" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>

</html>
