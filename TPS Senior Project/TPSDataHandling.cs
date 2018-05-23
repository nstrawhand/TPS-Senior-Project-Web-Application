using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace TPS_Senior_Project
{
    public class TPSDataHandling
    {
        string _ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=TPSDatabases\\database.accdb;Mode=ReadWrite;";
        OleDbConnection myConnection;
        OleDbCommand myCommand;
        OleDbDataReader myReader;
        OleDbDataAdapter myAdapter;

        // Initialization of the class call
        public TPSDataHandling()
        {
            try
            {
                // Initialize the database
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "CREATE TABLE login([userid] TEXT, [password] TEXT, [security] TEXT)";
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = "INSERT INTO login([userid], [password], [security]) VALUES ('root', 'password', 'M')";
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = "CREATE TABLE requests([requestid] AUTOINCREMENT PRIMARY KEY, [userid] TEXT, [staff] TEXT, [location] TEXT, [worktype] TEXT, [salary] TEXT, [status] TEXT)";
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = "CREATE TABLE staff([userid] TEXT, [full_name] TEXT, [degree] TEXT, [experience] TEXT, [salary] TEXT, [street] TEXT, [city] TEXT, [state] TEXT, [zipcode] TEXT)";
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = "INSERT INTO staff([userid], [full_name], [degree], [experience], [salary], [street], [city], [state], [zipcode]) VALUES ('root', '', '', '', '', '', '', '', '')";
                myCommand.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }

            // UNIT TEST
            /*System.Diagnostics.Debug.WriteLine("User Added? " + addUser("Test", "Test", "M").ToString());
            System.Diagnostics.Debug.WriteLine("User Existing? " + IsUserExists("Test").ToString());
            System.Diagnostics.Debug.WriteLine("User Security? " + validateUser("Test", "Test").ToString());
            System.Diagnostics.Debug.WriteLine("User Updatable? " + updateUser("Test", "123", "C").ToString());
            System.Diagnostics.Debug.WriteLine("User Deletable? " + deleteUser("Test").ToString());
            System.Diagnostics.Debug.WriteLine("Request Added? " + addRequest("Test", "Test", "Test", "Test", "Test").ToString());
            System.Diagnostics.Debug.WriteLine("Request Information? " + getRequest("1", "Test")["salary"].ToString());
            System.Diagnostics.Debug.WriteLine("Request Updatable? " + updateRequest("1", "Test", "Test2", "Test", "Test", "Test").ToString());
            System.Diagnostics.Debug.WriteLine("Last Known Request# By User? " + grabRequestNumber("Test").ToString());
            System.Diagnostics.Debug.WriteLine("Request Deletable? " + deleteRequest("1").ToString());
            System.Diagnostics.Debug.WriteLine("Staff Added? " + addStaff("Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test").ToString());
            System.Diagnostics.Debug.WriteLine("Staff Information? " + getStaff("Test")["full_name"].ToString());
            System.Diagnostics.Debug.WriteLine("Staff Updatable? " + updateStaff("Test", "Test", "Test2", "Test", "Test", "Test", "Test", "Test", "Test").ToString());
            System.Diagnostics.Debug.WriteLine("Staff Deletable? " + deleteStaff("Test").ToString());
            System.Diagnostics.Debug.WriteLine("Dataset Information? " + grabDataSet("SELECT * FROM login").Tables[0].Rows[0]["userid"].ToString());*/
        }

        // Handles creation of new login accounts, returns true if successful
        public bool addUser(string userid, string password, string security)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "INSERT INTO login([userid], [password], [security]) VALUES ('" + userid + "', '" + password + "', '" + security + "')";
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }

            return false;
        }

        // Method check for if the user exists
        public bool IsUserExists(string userid)
        {
            bool Result = false;
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "SELECT * FROM login WHERE [userid] = '" + userid + "'";

                using (myReader = myCommand.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        Result = true;
                    }
                }
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }
            return Result;
        }

        // Handles grabbing userinfo and sends back the security code if password info is correct
        public string validateUser(string userid, string password)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "SELECT * FROM login WHERE [userid] = '" + userid + "'";

                using (myReader = myCommand.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        if (myReader["password"].ToString() == password)
                        {
                            string returnVal = myReader["security"].ToString();
                            myConnection.Close();
                            return returnVal;
                        }
                    }
                }
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            myConnection.Close();
            return "";
        }

        // Handles updating user login accounts
        public bool updateUser(string userid, string password, string security)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "UPDATE login SET [password] = '" + password + "', [security] = '" + security + "' WHERE [userid] = '" + userid + "'";
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }

            return false;
        }

        // Handles deleting user login accounts
        public bool deleteUser(string userid)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "DELETE FROM login WHERE [userid] = '" + userid + "'";
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }

            return false;
        }

        // Handles creation of new staff requests
        public bool addRequest(string userid, string staff, string location, string worktype, string salary)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "INSERT INTO requests([userid], [staff], [location], [worktype], [salary], [status]) VALUES ('" + userid + "', '" + staff + "', '" + location + "', '" + worktype + "', '" + salary + "', 'Not yet reviewed')";
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }

            return false;
        }

        // Handles grabbing staff requests by requestid and sends back the info in a dictionary
        public Dictionary<string, string> getRequest(string requestid, string userid)
        {
            Dictionary<string, string> myDict = new Dictionary<string, string>();
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "SELECT * FROM requests WHERE [requestid] = " + requestid;

                using (myReader = myCommand.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        if (myReader["userid"].ToString() == userid)
                        {
                            myDict["staff"] =  myReader["staff"].ToString();
                            myDict["location"] = myReader["location"].ToString();
                            myDict["worktype"] = myReader["worktype"].ToString();
                            myDict["salary"] = myReader["salary"].ToString();
                            myDict["status"] = myReader["status"].ToString();
                            myConnection.Close();
                            return myDict;
                        }
                    }
                }
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return null;
        }

        // Handles updating staff requests
        public bool updateRequest(string requestid, string staff, string location, string worktype, string salary, string status)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "UPDATE requests SET [staff] = '" + staff + "', [location] = '" + location + "', [worktype] = '" + worktype + "', [salary] = '" + salary + "', [status] = '" + status + "' WHERE requestid = " + requestid;
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }

            return false;
        }

        // Handles deleting user login accounts
        public bool deleteRequest(string requestid)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "DELETE FROM requests WHERE [requestid] = " + requestid;
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }

            return false;
        }

        // Handles creation of staff info
        public bool addStaff(string userid, string full_name, string degree, string experience, string salary, string street, string city, string state, string zipcode)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "INSERT INTO staff([userid], [full_name], [degree], [experience], [salary], [street], [city], [state], [zipcode]) VALUES ('" + userid + "', '" + full_name + "', '" + degree + "', '" + experience + "', '" + salary + "', '" + street + "', '" + city + "', '" + state + "', '" + zipcode + "')";
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }

            return false;
        }

        // Handles grabbing staff info and sends back the info in a dictionary
        public Dictionary<string, string> getStaff(string userid)
        {
            Dictionary<string, string> myDict = new Dictionary<string, string>();
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "SELECT * FROM staff WHERE [userid] = '" + userid + "'";

                using (myReader = myCommand.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        myDict.Add("full_name", myReader["full_name"].ToString());
                        myDict.Add("degree", myReader["degree"].ToString());
                        myDict.Add("experience", myReader["experience"].ToString());
                        myDict.Add("salary", myReader["salary"].ToString());
                        myDict.Add("street", myReader["street"].ToString());
                        myDict.Add("city", myReader["city"].ToString());
                        myDict.Add("state", myReader["state"].ToString());
                        myDict.Add("zipcode", myReader["zipcode"].ToString());
                    }
                }
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            
            myConnection.Close();
            return myDict;
        }
        
        // Handles updating staff info
        public bool updateStaff(string userid, string full_name, string degree, string experience, string salary, string street, string city, string state, string zipcode)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "UPDATE staff SET [full_name] = '" + full_name +"', [degree] = '" + degree + "', [experience] = '" + experience + "', [salary] = '" + salary + "', [street] = '" + street + "', [city] = '" + city + "', [state] = '" + state + "', [zipcode] = '" + zipcode + "' WHERE userid = '" + userid + "'";
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }

            return false;
        }

        // Handles deleting staff info
        public bool deleteStaff(string userid)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "DELETE FROM staff WHERE [userid] = '" + userid + "'";
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }

            return false;
        }

        // Grabs the specific sql data and returns a dataset with the found information
        public DataSet grabDataSet(string sqlstring)
        {
            DataSet myDataSet = new DataSet();

            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = sqlstring;
                myAdapter = new OleDbDataAdapter(myCommand);
                myAdapter.Fill(myDataSet);
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
            }

            return myDataSet;
        }

        // Grabs the specific sql data and returns a dataset with the found information
        public string grabRequestNumber(string userid)
        {
            try
            {
                myConnection = new OleDbConnection(_ConnectionString);
                myConnection.Open();
                myCommand = new OleDbCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "SELECT max(requestid) AS [last] FROM requests WHERE userid = '" + userid + "'";
                using (myReader = myCommand.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        return myReader["last"].ToString();
                    }
                }
            }
            catch (OleDbException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
            }

            return "-1";
        }
    }
}