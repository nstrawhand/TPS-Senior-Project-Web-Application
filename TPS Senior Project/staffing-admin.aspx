<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="staffing-admin.aspx.cs" Inherits="TPS_Senior_Project.staffing_admin" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="header">
            <a href="index.aspx"><img src="Site Images/TPSHeader.png" /></a>
        </div>
        
        <div class="content-container">
            <div class="grid-overwrap">
                <asp:GridView ID="staff_grid" runat="server" AllowSorting="True" OnRowCreated="RowCreated" OnSorting="SortDataSet" CssClass="staff-request-table" OnSelectedIndexChanged="staff_grid_SelectedIndexChanged">
                </asp:GridView>
            </div>
            <asp:Panel ID="pnlControl" runat="server" Visible="False">
                <br />
                <asp:Label ID="Label1" runat="server" Text="Status"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem>Valid</asp:ListItem>
                    <asp:ListItem>Invalid</asp:ListItem>
                    <asp:ListItem>Unable to Fill</asp:ListItem>
                    <asp:ListItem>Filled</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" CssClass="button-field" />
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" CssClass="button-field" />
            </asp:Panel>

        </div>

        <div class="footer">
            <p>Webpage design &#169; 2018 by <a href="mailto:spauldingwm@gmail.com">William Spaulding</a>, Khahn Nguyen, <a href="Decrescentisr@gmail.com">Robert Descrescentis</a>, <a href="nathan.strawhand@cox.net">Nathan Strawhand</a>, and <a href="Camouflaged98002@yahoo.com">Norman Brandon</a> for CIS470 at <a href="http://www.tp.devry.edu/">DeVry Institute</a>.</p>
        </div>
    </div>
    </form>
</body>
</html>