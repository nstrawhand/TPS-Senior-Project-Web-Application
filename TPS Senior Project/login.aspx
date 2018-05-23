<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TPS_Senior_Project.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="header">
            <img src="Site Images/TPSHeader.png" />
        </div>
        
        <div class="login-container">
            <div class="login">
                <h1>Account Login</h1>
                <div class="credentials">
                    <asp:TextBox id="username" runat="server" name="username" placeholder="Username" CssClass="login-text" />
                    <asp:TextBox id="password" TextMode="password" runat="server" placeholder="Password" CssClass="login-text" />
                    <asp:Button runat="server" Text="Submit" ID="submit" OnClick="submit_Click" CssClass="login-submit" />
                </div>
            </div>
        </div>

        <div class="footer">
            <p>Webpage design &#169; 2018 by <a href="mailto:spauldingwm@gmail.com">William Spaulding</a>, Khahn Nguyen, <a href="Decrescentisr@gmail.com">Robert Descrescentis</a>, <a href="nathan.strawhand@cox.net">Nathan Strawhand</a>, and <a href="Camouflaged98002@yahoo.com">Norman Brandon</a> for CIS470 at <a href="http://www.tp.devry.edu/">DeVry Institute</a>.</p>
        </div>
    </div>
    </form>
</body>
</html>
