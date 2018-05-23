using System;

namespace TPS_Senior_Project
{
    public partial class Login : System.Web.UI.Page
    {
        TPSDataHandling useDatabase;

        protected void Page_Load(object sender, EventArgs e)
        {
            useDatabase = new TPSDataHandling();
            Session["UserID"] = "";
            Session["SecurityLevel"] = "";
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string SecurityLevel;
            SecurityLevel = useDatabase.validateUser(username.Text, password.Text);

            if (SecurityLevel != "")
            {
                Session["UserID"] = username.Text;
                Session["SecurityLevel"] = SecurityLevel;
                Server.Transfer("index.aspx", true);
                //System.Diagnostics.Debug.WriteLine("Can Valid User Info Login? True");
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine("Can Invalid User Info Login? False");
            }
        }
    }
}