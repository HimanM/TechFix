<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Supplier.aspx.cs" Inherits="Client.Supplier" %>
<%@ Register Src="Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
         <!-- Include the Navbar -->
        <uc:Navbar ID="Navbar1" runat="server" />
            <div class="row justify-content-md-center">
                <h2>Supplier List</h2>
                <div class="col-md-6 .offset-md-1">
                    <div class ="container mt-4 rounded border mb-2 p-3">
                        <asp:GridView ID="SupplierGrid" runat="server" AutoGenerateColumns="True" AllowPaging="True" PageSize="10" CssClass="table table-striped" OnPageIndexChanging="SupplierGrid_PageIndexChanging"/>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class ="container mt-4 rounded border mb-2 p-3">
                        <div class="row mb-2">
                            <div class="col-md-12">
                                <asp:Label ID="lblMessage" runat="server" Text="" CssClass="form-text"></asp:Label>
                            </div>
                        </div>

                        <div class="row mb-2">
                            <div class="col-md-6">
                                <asp:DropDownList ID="ddlSuppliers" runat="server" AutoPostBack="True" 
                                                    OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged"
                                                    CssClass="form-select"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label for="txtName" class="form-label">Name:</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                ErrorMessage="Name is required" CssClass="text-danger"
                                ValidationGroup="SupplierValidation" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label for="txtEmail" class="form-label">Contact Email:</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Email is required" CssClass="text-danger"
                                ValidationGroup="SupplierValidation" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label for="txtPhone" class="form-label">Contact Phone:</label>
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhone"
                                ErrorMessage="Contact Phone is required" CssClass="text-danger"
                                ValidationGroup="SupplierValidation" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label for="txtAddress" class="form-label">Address:</label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAddress"
                                ErrorMessage="Address is required" CssClass="text-danger"
                                ValidationGroup="SupplierValidation" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label for="txtWebsite" class="form-label">Website URL:</label>
                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtWebsite"
                                ErrorMessage="Website is required" CssClass="text-danger"
                                ValidationGroup="SupplierValidation" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6 d-flex justify-content-start gap-2">
                                <asp:Button ID="btnAdd" runat="server" Text="Add Supplier" CssClass="btn btn-primary" OnClick="btnAdd_Click" ValidationGroup="SupplierValidation"/>
                                <asp:Button ID="btnUpdate" runat="server" Text="Update Supplier" CssClass="btn btn-warning" OnClick="btnUpdate_Click" ValidationGroup="SupplierValidation"/>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete Supplier" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
