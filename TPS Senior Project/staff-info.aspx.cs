using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPS_Senior_Project
{
    public partial class staff_info : System.Web.UI.Page
    {
        TPSDataHandling tpsData;
        DataSet ds;
        string selectedStaff;

        // Handles when the pages loads
        protected void Page_Load(object sender, EventArgs e)
        {
            // Security Check
            if (Session["SecurityLevel"].ToString() == "S")
            {
                Server.Transfer("index.aspx", true);
            }
            else if (Session["SecurityLevel"].ToString() == "M" || Session["SecurityLevel"].ToString() == "C")
            {
                // Do nothing
            }
            else
            {
                Server.Transfer("login.aspx", true);
            }

            // Grab a dataset
            tpsData = new TPSDataHandling();
            ds = tpsData.grabDataSet("SELECT staff.[userid] AS [ID], [full_name] AS [Name], [experience] AS [Experience (Years)], [degree] AS [Degree], [salary] AS [Salary], [city] AS [City], [state] AS [State] FROM staff, login WHERE login.[userid] = staff.[userid] AND [security] = 'S'");
            // Check for postback data in case of changes
            if (!Page.IsPostBack)
            {
                Session["DataView"] = new DataView(ds.Tables[0]);
                staff_grid.DataSource = ds;
                Session["Staff"] = "";
            }
            else
            {
                selectedStaff = Session["Staff"].ToString();
                staff_grid.DataSource = (DataView)Session["DataView"];
            }

            // Bind data
            staff_grid.DataBind();
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
            Session["DataView"] = dv;
            if (staff_grid.SelectedIndex != -1)
            {
                updateSelected();
            }
        }

        // Method to handle selecting a line item on the gridview
        protected void staff_grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedStaff = staff_grid.Rows[staff_grid.SelectedIndex].Cells[0].Text;
            Session["Staff"] = selectedStaff;
            updateSelected();
        }

        // Method that handles which row is highlighted
        protected void updateSelected()
        {
            lblName.Text = staff_grid.Rows[staff_grid.SelectedIndex].Cells[1].Text;

            imgProfile.Visible = false;
            lbResume.Visible = false;

            string[] dirs = Directory.GetFiles(Server.MapPath("Pictures") + "\\");
            foreach (string dir in dirs)
            {
                if (dir.Contains(selectedStaff))
                {
                    imgProfile.ImageUrl = "~/Pictures/" + selectedStaff + "." + Path.GetExtension(dir).Substring(1);
                    imgProfile.Visible = true;
                }
            }

            dirs = Directory.GetFiles(Server.MapPath("Resume") + "\\");
            foreach (string dir in dirs)
            {
                if (dir.Contains(selectedStaff))
                {
                    lbResume.Visible = true;
                }
            }

            pnlInfo.Visible = true;

            // Set row backcolor
            foreach (GridViewRow row in staff_grid.Rows)
            {
                if (row.Cells[0].Text == selectedStaff)
                {
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4286f4");
                }
            }
        }

        protected void lbResume_Click(object sender, EventArgs e)
        {
            string[] dirs = Directory.GetFiles(Server.MapPath("Resume") + "\\");
            foreach (string dir in dirs)
            {
                if (dir.Contains(staff_grid.Rows[staff_grid.SelectedIndex].Cells[0].Text))
                {
                    FileInfo Dfile = new System.IO.FileInfo(dir);
                    string name = Dfile.FullName; //get file name
                    string ext = Dfile.Extension; //get file extension
                    string type = "";

                    // set known types based on file extension
                    if (ext != null)
                    {
                        switch (ext.ToLower())
                        {
                            case ".htm":
                            case ".html":
                                type = "text/HTML";
                                break;

                            case ".txt":
                                type = "text/plain";
                                break;

                            case ".GIF":
                                type = "image/GIF";
                                break;

                            case ".pdf":
                                type = "Application/pdf";
                                break;

                            case ".doc":
                            case ".rtf":
                                type = "Application/msword";
                                break;
                        }
                    }

                    Response.AppendHeader("content-disposition", "attachment; filename=" + name);

                    if (type != "")
                    {
                        Response.ContentType = type;
                    }

                    Response.WriteFile(dir);
                    Response.End();
                }
            }
        }
    }
}