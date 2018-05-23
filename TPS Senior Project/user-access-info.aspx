<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user-access-info.aspx.cs" Inherits="TPS_Senior_Project.user_access_info" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="style.css" />
    <style>
        .tdalign {
            text-align: right;
        }
        .tdalignl {
            text-align: left;
        }
        input[type="submit"]
        {
            cursor: pointer;
            top: 1px;
        }
        #lblMessage
        {
           
        }
        .hover_row{
            background-color:#6f7175;
            color:white;
        }
    </style>

    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$('#lblMessage').show("slow");
        });
        $(function () {
            $("[id*=GridView1] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>
</head>
<body>
    
    <form id="form1" runat="server" autocomplete="off">
     <asp:LinkButton ID="hdnfklInk" runat="server"></asp:LinkButton>
        <div class="wrapper">
            <div class="header">
                <a href="index.aspx">
                    <img src="Site Images/TPSHeader.png" /></a>
            </div>

            <div class="content-container">

                <br />
                            <asp:Label ID="Label2" runat="server" Text="User Name:"></asp:Label>
                <asp:TextBox ID="userid" runat="server" CssClass="text-field"></asp:TextBox>
                <br />
                            <asp:Label ID="Label3" runat="server" autocomplete="off" Text="Password:"></asp:Label>
                <asp:TextBox ID="password" runat="server" CssClass="text-field"></asp:TextBox>
                <br />
                            <asp:Label ID="Label4" runat="server" Text="Security Level:"></asp:Label>
                <br />
                            <asp:DropDownList ID="DropDownList_Level" runat="server">
                                <asp:ListItem Value="C">Client</asp:ListItem>
                                <asp:ListItem Value="M">Manager</asp:ListItem>
                                <asp:ListItem Value="S">Staff</asp:ListItem>
                            </asp:DropDownList> <asp:Button ID="btn_AddUsers" runat="server" OnClick="btn_AddUsers_Click" Text="Add User" CssClass="button-field" /> <asp:Button ID="btnDeleteUser" OnClick="btnDeleteUser_Click" runat="server"  Text="Delete User " CssClass="button-field" /> 
                            <asp:Button ID="btn_Update" OnClick="btn_Update_Click" runat="server" Text="Update " CssClass="button-field" />
                <br />
                            <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" ForeColor="#ec5858"></asp:Label>
                 <div class="grid-overwrap">
                               <asp:GridView ID="user_grid" runat="server" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="gray" OnRowDataBound="user_grid_RowDataBound" OnSelectedIndexChanged="user_grid_SelectedIndexChanged" Width="591px" CssClass="staff-request-table">

<HeaderStyle BackColor="Black" ForeColor="Gray"></HeaderStyle>
                               </asp:GridView>
                 </div>
            </div>

            <div class="footer">
                <p>Webpage design &#169; 2018 by <a href="mailto:spauldingwm@gmail.com">William Spaulding</a>, Khahn Nguyen, <a href="Decrescentisr@gmail.com">Robert Descrescentis</a>, <a href="nathan.strawhand@cox.net">Nathan Strawhand</a>, and <a href="Camouflaged98002@yahoo.com">Norman Brandon</a> for CIS470 at <a href="http://www.tp.devry.edu/">DeVry Institute</a>.</p>
            </div>
        </div>
    </form>
</body>
</html>