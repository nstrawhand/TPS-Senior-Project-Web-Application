using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace TPS_Senior_Project
{
    public partial class profile : System.Web.UI.Page
    {
        TPSDataHandling tpsDataHandling;
        Dictionary<string, string> myDict;

        // Method for when page loads
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SecurityLevel"].ToString() == "C")
            {
                Server.Transfer("index.aspx", true);
            }
            else if (Session["SecurityLevel"].ToString() == "S" || Session["SecurityLevel"].ToString() == "M")
            {
                // Do nothing
            }
            else
            {
                Server.Transfer("login.aspx", true);
            }

            tpsDataHandling = new TPSDataHandling();

            if (!IsPostBack)
            {
                myDict = tpsDataHandling.getStaff(Session["UserID"].ToString());

                txtName.Text = myDict["full_name"];

                if (ddlDegree.Items.Contains(new ListItem(myDict["degree"])))
                {
                    ddlDegree.SelectedValue = myDict["degree"];
                }


                txtExperience.Text = myDict["experience"];
                txtSalary.Text = myDict["salary"];
                txtStreet.Text = myDict["street"];
                txtCity.Text = myDict["city"];
                txtState.Text = myDict["state"];
                txtZipcode.Text = myDict["zipcode"];
            }

            string[] dirs = Directory.GetFiles(Server.MapPath("Pictures") + "\\");
            foreach (string dir in dirs)
            {
                if (dir.Contains(Session["UserID"].ToString()))
                {
                    imgProfile.ImageUrl = "~/Pictures/" + Session["UserID"].ToString() + "." + Path.GetExtension(dir).Substring(1);
                    imgProfile.Visible = true;
                }
            }

            dirs = Directory.GetFiles(Server.MapPath("Resume") + "\\");
            foreach (string dir in dirs)
            {
                if (dir.Contains(Session["UserID"].ToString()))
                {
                    lbResume.Visible = true;
                }
            }
        }

        // Method occurs when update button clicked
        protected void updateProfile(object sender, EventArgs e)
        {
            // Database entry
            string message = "Your information has been updated accoridngly!<br><br>";
            double number;
            if (txtName.Text.Equals("") || ddlDegree.SelectedValue.Equals("") || txtExperience.Text.Equals("") || !Double.TryParse(txtExperience.Text, out number) || txtSalary.Text.Equals("") || txtStreet.Text.Equals("") || txtCity.Text.Equals("") || txtState.Text.Equals("") || txtZipcode.Text.Equals(""))
            {
                message += "However:<br>There was an error found in your entry fields, resulting in a failure to store field information. Make sure that all fields are filled and that the Experience field is a double value.";
            }
            else
            {
                tpsDataHandling.updateStaff(Session["UserID"].ToString(), txtName.Text, ddlDegree.SelectedValue, txtExperience.Text, txtSalary.Text, txtStreet.Text, txtCity.Text, txtState.Text, txtZipcode.Text);
            }

            // Picture upload
            HttpPostedFile file = (HttpPostedFile)(fileupPicture.PostedFile);
            // File must be less than 1 MB
            if (file != null && IsImage(file))
            {
                Bitmap bitmp = new Bitmap(file.InputStream);
                // File must have dimensions <= 200x200
                if (bitmp.Width <= 200 && bitmp.Height <= 200)
                {
                    try
                    {
                        string fn = Path.GetFileName(file.FileName);
                        string FileExtension = "." + Path.GetExtension(file.FileName).Substring(1);
                        string SaveLocation = Server.MapPath("Pictures") + "\\" + Session["UserID"].ToString() + FileExtension;
                        file.SaveAs(SaveLocation);

                        string[] dirs = Directory.GetFiles(Server.MapPath("Pictures") + "\\");

                        foreach (string dir in dirs)
                        {
                            if (dir.Contains(Session["UserID"].ToString()) && !dir.Equals(SaveLocation))
                            {
                                File.Delete(dir);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (message.Equals("Your information has been updated accoridngly!<br><br>"))
                        {
                            message += "However:";
                        }
                        message += "<br>The image selected was unable to upload. ";
                    }
                }
                else
                {
                    if (message.Equals("Your information has been updated accoridngly!<br><br>"))
                    {
                        message += "However:";
                    }
                    message += "<br>The image failed to upload since it was not less than or equal to 200x200. ";
                }
            }
            else
            {
                if (!imgProfile.Visible)
                {
                    if (message.Equals("Your information has been updated accoridngly!<br><br>"))
                    {
                        message += "However:";
                    }
                    message += "<br>There was either an error uploading an image, or you have yet to upload one.";
                }
            }

            // Resume Upload
            file = (HttpPostedFile)(fileupResume.PostedFile);
            // File must be less than 1 MB
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string fn = Path.GetFileName(file.FileName);
                    string FileExtension = "." + Path.GetExtension(file.FileName).Substring(1);
                    string SaveLocation = Server.MapPath("Resume") + "\\" + Session["UserID"].ToString() + FileExtension;
                    file.SaveAs(SaveLocation);

                    string[] dirs = Directory.GetFiles(Server.MapPath("Resume") + "\\");

                    foreach (string dir in dirs)
                    {
                        if (dir.Contains(Session["UserID"].ToString()) && !dir.Equals(SaveLocation))
                        {
                            File.Delete(dir);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    if (message.Equals("Your information has been updated accoridngly!<br><br>"))
                    {
                        message += "However:";
                    }
                    message += "<br>The resume selected was unable to upload. ";
                }
            }
            else
            {
                if (!lbResume.Visible)
                {
                    if (message.Equals("Your information has been updated accoridngly!<br><br>"))
                    {
                        message += "However:";
                    }
                    message += "<br>There was either an error uploading a resume, or you have yet to upload one.";
                }
            }

            // Pass page success info
            Session["Title"] = "<h1>Information Updated</h1>";
            Session["Message"] = message;
            Server.Transfer("success.aspx", true);
        }

        // Methoc checks for if a file in an image
        private bool IsImage(HttpPostedFile file)
        {
            // This checks for image type... you could also do filename extension checks and other things
            // but this is just an example to get you on your way
            return ((file != null) && System.Text.RegularExpressions.Regex.IsMatch(file.ContentType, "image/\\S+") && (file.ContentLength > 0));
        }

        // Method to handle viewing the resume
        protected void lbResume_Click(object sender, EventArgs e)
        {

            string[] dirs = Directory.GetFiles(Server.MapPath("Resume") + "\\");
            foreach (string dir in dirs)
            {
                if (dir.Contains(Session["UserID"].ToString()))
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

        protected void valueChange(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                Session[((TextBox)sender).ID] = ((TextBox)sender).Text;
            }
            else if (sender is DropDownList)
            {
                Session[((DropDownList)sender).ID] = ((DropDownList)sender).SelectedValue;
            }
        }
    }
}