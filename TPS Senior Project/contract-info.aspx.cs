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
    public partial class contract_info : System.Web.UI.Page
    {
        string selectedValue;
        string[] dirs;
        DataSet ds;

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

            DataTable myTable = new DataTable("Contracts");
            myTable.Columns.Add("Contracts");
            dirs = Directory.GetFiles(Server.MapPath("Contracts") + "\\");
            foreach (string dir in dirs)
            {
                string fn = Path.GetFileName(dir);
                myTable.Rows.Add(fn);
            }

            ds = new DataSet("Contracts");
            ds.Tables.Add(myTable);

            if (!IsPostBack)
            {
                staff_grid.DataSource = ds;
                staff_grid.DataBind();
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
            btnOpen.Visible = true;
            btnDelete.Visible = true;
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string[] dirs = Directory.GetFiles(Server.MapPath("Contracts") + "\\");
            foreach (string dir in dirs)
            {
                if (dir.Contains(staff_grid.Rows[staff_grid.SelectedIndex].Cells[0].Text))
                {
                    File.Delete(dir);

                    Session["Title"] = "<h1>Contract Deleted!</h1>";
                    Session["Message"] = "The specified contract file has been deleted from the system.";
                    Server.Transfer("success.aspx", true);
                    return;
                }
            }
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string[] dirs = Directory.GetFiles(Server.MapPath("Contracts") + "\\");
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

                    return;
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            // Resume Upload
            HttpPostedFile file = (HttpPostedFile)(fileupContract.PostedFile);
            // File must be less than 1 MB
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string fn = Path.GetFileName(file.FileName);
                    string SaveLocation = Server.MapPath("Contracts") + "\\" + fn;

                    string[] dirs = Directory.GetFiles(Server.MapPath("Contracts") + "\\");

                    foreach (string dir in dirs)
                    {
                        if (dir.Contains(fn))
                        {
                            File.Delete(dir);
                            break;
                        }
                    }

                    file.SaveAs(SaveLocation);
                    Session["Title"] = "<h1>Contract Uploaded!</h1>";
                    Session["Message"] = "The contract file has been uploaded to the system. You may now reference it from the contract-info page at any time.";
                    Server.Transfer("success.aspx", true);
                    return;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}