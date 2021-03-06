﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contract-info.aspx.cs" Inherits="TPS_Senior_Project.contract_info" EnableEventValidation="false" %>

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
                <asp:GridView ID="staff_grid" runat="server" AllowSorting="True" CssClass="staff-request-table" OnRowCreated="RowCreated" OnSelectedIndexChanged="staff_grid_SelectedIndexChanged" OnSorting="SortDataSet">
                </asp:GridView>
            </div>
            <asp:Button ID="btnOpen" runat="server" Text="Open" Visible="False" OnClick="btnOpen_Click" CssClass="button-field" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" CssClass="button-field" />
            <br /><asp:FileUpload ID="fileupContract" runat="server" CssClass="center" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="button-field" />
        </div>

        <div class="footer">
            <p>Webpage design &#169; 2018 by <a href="mailto:spauldingwm@gmail.com">William Spaulding</a>, Khahn Nguyen, <a href="Decrescentisr@gmail.com">Robert Descrescentis</a>, <a href="nathan.strawhand@cox.net">Nathan Strawhand</a>, and <a href="Camouflaged98002@yahoo.com">Norman Brandon</a> for CIS470 at <a href="http://www.tp.devry.edu/">DeVry Institute</a>.</p>
        </div>
    </div>
    </form>
</body>
</html>