<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Client.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TechFix Solution</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <style>
        body {
            background-color: #f8f9fa;
        }

        .hero {
            background-color: #343a40;
            color: white;
            padding: 60px 0;
        }

        .logo-placeholder {
            height: 150px;
            width: 150px;
            background-color: #dee2e6;
            border-radius: 50%;
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 1.2rem;
            color: #6c757d;
        }

        .project-info {
            padding: 40px 0;
        }
    </style>

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
         <header class="hero text-center">
            <div class="container">
                <div class="logo-placeholder mx-auto mb-4">
                    <asp:Image ID="Logo" runat="server" ImageUrl="~/images/logo.png" AlternateText="Logo" CssClass="img-fluid" />
                </div>
                <h1>TechFix Solution</h1>
                <p class="lead">A Solution Developed for TechFix Company</p>
            </div>
        </header>

        <section class="project-info text-center">
            <div class="container">
                <h2>Developed By</h2>
                <!-- Using ASP.NET Literal Control to display dynamic text -->
                <asp:Literal ID="LiteralName" runat="server" Text="Himan Manduja"></asp:Literal>
                <p class="fs-5">
                    Student ID: 
                    <asp:Literal ID="LiteralStudentID" runat="server" Text="112/124"></asp:Literal>
                </p>
            </div>
        </section>

        <footer class="text-center py-4">
            <div class="container">
                <p>&copy; Himan Manduja CL/HDCSE/CMU/112/124 </p>
            </div>
        </footer>
    </form>
</body>
</html>
