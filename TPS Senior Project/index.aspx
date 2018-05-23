<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TPS_Senior_Project.index" %>

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
            <a href="login.aspx"><img src="Site Images/TPSHeader.png" /></a>
        </div>

        <div class="content-nav">
            <asp:Panel ID="Panel1" runat="server" CssClass="control-field">
                <a href="staffing-request.aspx"><img src="Site Images/request.png" /></a>
                <h1><a href="staffing-request.aspx">Staffing Request</a></h1>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" CssClass="control-field">
                <a href="staffing-request-info.aspx"><img src="Site Images/request-info.png" /></a>
                <h1><a href="staffing-request-info.aspx">Staffing Request Information</a></h1>
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server" CssClass="control-field">
                <a href="staffing-admin.aspx"><img src="Site Images/request-admin.png" /></a>
                <h1><a href="staffing-admin.aspx">Staffing Administration</a></h1>
            </asp:Panel>
            <asp:Panel ID="Panel4" runat="server" CssClass="control-field">
                <a href="contract-info.aspx"><img src="Site Images/contract.png" /></a>
                <h1><a href="contract-info.aspx">Contract Information</a></h1>
            </asp:Panel>
            <asp:Panel ID="Panel5" runat="server" CssClass="control-field">
                <a href="profile.aspx"><img src="Site Images/profile.png" /></a>
                <h1><a href="profile.aspx">Edit Profile</a></h1>
            </asp:Panel>
            <asp:Panel ID="Panel6" runat="server" CssClass="control-field">
                <a href="staff-info.aspx"><img src="Site Images/staff-info.png" /></a>
                <h1><a href="staff-info.aspx">Staff Information</a></h1>
            </asp:Panel>
            <asp:Panel ID="Panel7" runat="server" CssClass="control-field">
                <a href="user-access-info.aspx"><img src="Site Images/user-access.png" /></a>
                <h1><a href="user-access-info.aspx">User Access Information</a></h1>
            </asp:Panel>
        </div>

        <div class="footer">
            <p>Webpage design &#169; 2018 by <a href="mailto:spauldingwm@gmail.com">William Spaulding</a>, Khahn Nguyen, <a href="Decrescentisr@gmail.com">Robert Descrescentis</a>, <a href="nathan.strawhand@cox.net">Nathan Strawhand</a>, and <a href="Camouflaged98002@yahoo.com">Norman Brandon</a> for CIS470 at <a href="http://www.tp.devry.edu/">DeVry Institute</a>.</p>
        </div>
    </div>
    </form>
</body>
</html>
