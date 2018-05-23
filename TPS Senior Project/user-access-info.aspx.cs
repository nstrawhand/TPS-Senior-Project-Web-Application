using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPS_Senior_Project
{
    public partial class user_access_info : System.Web.UI.Page
    {
        TPSDataHandling userDatabase;
        DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SecurityLevel"].ToString() == "M")
            {
                //Do nothing
            }
            else if (Session["SecurityLevel"].ToString() == "S" || Session["SecurityLevel"].ToString() == "C")
            {
                Server.Transfer("index.aspx", true);
            }
            else
            {
                Server.Transfer("login.aspx", true);
            }

            userDatabase = new TPSDataHandling();

            ds = userDatabase.grabDataSet("SELECT * FROM login");
            user_grid.DataSource = ds;
            user_grid.DataBind();
        }

        protected void btn_AddUsers_Click(object sender, EventArgs e)
        {
            var userIdTxt = userid.Text.Trim();
            if (!password.Text.Trim().Equals(string.Empty) && !userIdTxt.Equals(string.Empty))
            {
                if (new TPSDataHandling().IsUserExists(userIdTxt))
                {
                    lblMessage.Text = "UserId(" + userIdTxt + ") already exists.";
                    return;
                }

                bool Done = new TPSDataHandling().addUser(userIdTxt, password.Text, DropDownList_Level.SelectedValue);
                if (Done)
                {
                    if (DropDownList_Level.SelectedValue.Equals("S"))
                    {
                        userDatabase.addStaff(userIdTxt, "", "", "", "", "", "", "", "");
                    }

                    lblMessage.Text = "User(" + userIdTxt + ") was successfully added!";
                    ds = userDatabase.grabDataSet("SELECT * FROM [login]");
                    user_grid.DataSource = ds;
                    user_grid.DataBind();
                }
                else
                {
                    lblMessage.Text = "The user could not be added!";
                }
            }
            else
            {
                lblMessage.Text = "Please enter valid userName and password!";
            }

        }

        protected void user_grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(user_grid, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Please click to select this row.";
            }
        }

        protected void user_grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in user_grid.Rows)
            {
                if (row.RowIndex == user_grid.SelectedIndex)
                {
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#5b7ef0");
                    row.ToolTip = string.Empty;

                    userid.Text = row.Cells[0].Text;
                    password.Text = row.Cells[1].Text;
                    DropDownList_Level.SelectedValue = row.Cells[2].Text;
                }
                else
                {
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#001a28");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            var tps = new TPSDataHandling();
            if (!userid.Text.Trim().Equals(string.Empty))
            {
                var userIdTxt = userid.Text.Trim();
                if (tps.deleteUser(userIdTxt))
                {
                    if (DropDownList_Level.SelectedValue.Equals("S"))
                    {
                        tps.deleteStaff(userIdTxt);
                    }

                    lblMessage.Text = "UserId(" + userIdTxt + ") was successfully deleted.";
                    ds = userDatabase.grabDataSet("SELECT * FROM [login]");
                    user_grid.DataSource = ds;
                    user_grid.DataBind();
                }
            }
            else
            {
                lblMessage.Text = "Invalid UserId.";
            }
            userid.Text = "";
            password.Text = "";
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            var tps = new TPSDataHandling();
            var userIdTxt = userid.Text.Trim();
            if (!userIdTxt.Equals(string.Empty))
            {
                if (!new TPSDataHandling().IsUserExists(userIdTxt))
                {
                    lblMessage.Text = "Update :: UserId(" + userIdTxt + ") not found.";
                    return;
                }

                if (tps.updateUser(userIdTxt, password.Text, DropDownList_Level.SelectedValue))
                {
                    lblMessage.Text = "UserId(" + userIdTxt + ") successfully updated.";
                    ds = userDatabase.grabDataSet("SELECT * FROM [login]");
                    user_grid.DataSource = ds;
                    user_grid.DataBind();
                }
                else
                {
                    lblMessage.Text = "Failed to update UserId(" + userIdTxt + ").";
                }
            }
            else
            {
                lblMessage.Text = "Invalid UserId.";
            }
        }

    }
}