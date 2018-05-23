using System;
using System.Data;
using System.Web.UI.WebControls;

namespace TPS_Senior_Project
{
    public partial class staffing_admin : System.Web.UI.Page
    {
        TPSDataHandling tpsData;
        DataSet ds;
        string selectedValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SecurityLevel"].ToString() == "M")
            {
                // Do nothing
            }
            else if (Session["SecurityLevel"].ToString() == "S" || Session["SecurityLevel"].ToString() == "C")
            {
                Server.Transfer("index.aspx", true);
            }
            else
            {
                Server.Transfer("login.aspx", true);
            }

            tpsData = new TPSDataHandling();
            ds = tpsData.grabDataSet("SELECT * FROM requests");
            if (!IsPostBack)
            {
                staff_grid.DataSource = ds;
                staff_grid.DataBind();
                selectedValue = "";
            }
        }

        // Method to handle tooltips and cursor for rows, as well as selection method
        protected void RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(staff_grid, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';";
                e.Row.ToolTip = "Click to select row";
            }
        }

        // Sorts datagridview when clicking headers
        protected void SortDataSet(object sender, GridViewSortEventArgs e)
        {
            // Dataview allows for sorting given data
            DataView dv = new DataView(ds.Tables[0]);

            // Change according to session variable column header, according to last known direction
            if (Session[e.SortExpression.ToString()] != null && Session[e.SortExpression.ToString()].ToString() == "ASC")
            {
                dv.Sort = e.SortExpression.ToString() + " DESC";
                Session[e.SortExpression.ToString()] = "DESC";
            }
            else
            {
                dv.Sort = e.SortExpression.ToString() + " ASC";
                Session[e.SortExpression.ToString()] = "ASC";
            }

            // Bind new data and set postback data
            staff_grid.DataSource = dv;
            staff_grid.DataBind();
            // Call to show current selected staff
            updateSelected();
        }

        // Method to handle selecting a line item on the gridview
        protected void staff_grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedValue = staff_grid.Rows[staff_grid.SelectedIndex].Cells[0].Text;
            pnlControl.Visible = true;
            updateSelected();
        }

        // Method that handles which row is highlighted
        protected void updateSelected()
        {
            // Set row backcolor
            foreach (GridViewRow row in staff_grid.Rows)
            {
                if (row.Cells[0].Text == selectedValue)
                {
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4286f4");
                }
                else
                {
                    row.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }

        // Method to delete staff requests
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            tpsData.deleteRequest(staff_grid.Rows[staff_grid.SelectedIndex].Cells[0].Text);
            Session["Title"] = "<h1>Request Deleted</h1>";
            Session["Message"] = "The request has been deleted. Please contact TPS for any and all questions. Thank you and have a great day!";
            Server.Transfer("success.aspx", true);
        }

        // Method to update staff requests
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            tpsData.updateRequest(staff_grid.Rows[staff_grid.SelectedIndex].Cells[0].Text, staff_grid.Rows[staff_grid.SelectedIndex].Cells[2].Text, staff_grid.Rows[staff_grid.SelectedIndex].Cells[3].Text, staff_grid.Rows[staff_grid.SelectedIndex].Cells[4].Text, staff_grid.Rows[staff_grid.SelectedIndex].Cells[5].Text, ddlStatus.SelectedValue);
            Session["Title"] = "<h1>Request Updated</h1>";
            Session["Message"] = "The request has been updated. Please contact TPS for any and all questions. Thank you and have a great day!";
            Server.Transfer("success.aspx", true);
        }
    }
}