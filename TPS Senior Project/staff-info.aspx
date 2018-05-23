<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="staff-info.aspx.cs" Inherits="TPS_Senior_Project.staff_info" EnableEventValidation="false" %>

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
            
            <div class="staff-listing">
                <br />
                <asp:Label ID="Label1" runat="server" Text="Select a row below to view the associated picture and resume! If one or another does not show up, this is due to the user still needing to submit their information!" Width="700px"></asp:Label>
                <br />
                <br />
                <div class="grid-overwrap">
                    <asp:GridView ID="staff_grid" runat="server" CssClass="staff-request-table" OnRowCreated="RowCreated" AllowSorting="True" OnSorting="SortDataSet" OnSelectedIndexChanged="staff_grid_SelectedIndexChanged">
                    </asp:GridView>
                </div>
            </div>

            <asp:Panel ID="pnlInfo" runat="server" Visible="False">
                <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                <asp:Image ID="imgProfile" runat="server" />
                <br />
                <asp:LinkButton ID="lbResume" runat="server" OnClick="lbResume_Click">Resume</asp:LinkButton>
            </asp:Panel>
        </div>

        <div class="footer">
            <p>Webpage design &#169; 2018 by <a href="mailto:spauldingwm@gmail.com">William Spaulding</a>, Khahn Nguyen, <a href="Decrescentisr@gmail.com">Robert Descrescentis</a>, <a href="nathan.strawhand@cox.net">Nathan Strawhand</a>, and <a href="Camouflaged98002@yahoo.com">Norman Brandon</a> for CIS470 at <a href="http://www.tp.devry.edu/">DeVry Institute</a>.</p>
        </div>
    </div>
    </form>
</body>
</html>