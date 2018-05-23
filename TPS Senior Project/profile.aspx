<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="TPS_Senior_Project.profile" %>

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
            
            <br />
            
            <asp:Label ID="Label3" runat="server" Text="Full Name"></asp:Label>
            
            <asp:TextBox ID="txtName" runat="server" OnTextChanged="valueChange" CssClass="text-field"></asp:TextBox>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Degree:"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlDegree" runat="server" OnSelectedIndexChanged="valueChange">
                <asp:ListItem>Highschool</asp:ListItem>
                <asp:ListItem>Associate</asp:ListItem>
                <asp:ListItem>Bachelor</asp:ListItem>
                <asp:ListItem>Master</asp:ListItem>
                <asp:ListItem>Doctoral</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Experience (Years)"></asp:Label>
            <asp:TextBox ID="txtExperience" runat="server" OnTextChanged="valueChange" CssClass="text-field" ></asp:TextBox>
            <br />
            <asp:Label ID="Label6" runat="server" Text="Salary"></asp:Label>
            <br />
            <asp:TextBox ID="txtSalary" runat="server" OnTextChanged="valueChange" CssClass="text-field" ></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Address"></asp:Label>
            <asp:TextBox ID="txtStreet" runat="server" placeholder="Street" OnTextChanged="valueChange" CssClass="text-field" ></asp:TextBox>
            <asp:TextBox ID="txtCity" runat="server" placeholder="City" OnTextChanged="valueChange" CssClass="text-field" ></asp:TextBox>
            <asp:TextBox ID="txtState" runat="server" placeholder="State" OnTextChanged="valueChange" CssClass="text-field" ></asp:TextBox>
            <asp:TextBox ID="txtZipcode" runat="server" placeholder="Zip" OnTextChanged="valueChange" CssClass="text-field" ></asp:TextBox>
            
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Resume"></asp:Label>
            <br />
            <asp:LinkButton ID="lbResume" runat="server" OnClick="lbResume_Click" Visible="False">Resume</asp:LinkButton>
            <br />
            
            <asp:FileUpload ID="fileupResume" runat="server" CssClass="center" />
            <br />
            <asp:Label ID="Label8" runat="server" Text="Picture"></asp:Label>
            <br />
            <asp:Image ID="imgProfile" runat="server" Visible="False" />
            <asp:FileUpload ID="fileupPicture" runat="server" CssClass="center" />
            <br />
            <asp:Button ID="btnUpdate" runat="server" OnClick="updateProfile" Text="Update" CssClass="button-field" />
            
        </div>

        <div class="footer">
            <p>Webpage design &#169; 2018 by <a href="mailto:spauldingwm@gmail.com">William Spaulding</a>, Khahn Nguyen, <a href="Decrescentisr@gmail.com">Robert Descrescentis</a>, <a href="nathan.strawhand@cox.net">Nathan Strawhand</a>, and <a href="Camouflaged98002@yahoo.com">Norman Brandon</a> for CIS470 at <a href="http://www.tp.devry.edu/">DeVry Institute</a>.</p>
        </div>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
