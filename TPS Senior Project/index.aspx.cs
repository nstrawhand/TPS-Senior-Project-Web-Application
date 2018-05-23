using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPS_Senior_Project
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SecurityLevel"].ToString() == "S")
            {
                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
                Panel5.Visible = true;
                Panel6.Visible = false;
                Panel7.Visible = false;
            }
            else if (Session["SecurityLevel"].ToString() == "M")
            {
                Panel1.Visible = true;
                Panel2.Visible = true;
                Panel3.Visible = true;
                Panel4.Visible = true;
                Panel5.Visible = true;
                Panel6.Visible = true;
                Panel7.Visible = true;
            }
            else if (Session["SecurityLevel"].ToString() == "C")
            {
                Panel1.Visible = true;
                Panel2.Visible = true;
                Panel3.Visible = false;
                Panel4.Visible = false;
                Panel5.Visible = false;
                Panel6.Visible = true;
                Panel7.Visible = false;
            }
            else
            {
                Server.Transfer("login.aspx", true);
            }
        }
    }
}