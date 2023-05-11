using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        

        string strConnString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        string oldName ;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddUser(FormCollection post)
        {
            DataTable dt = new DataTable();
            string username = post["UserName"];
            string age = post["Age"] ;
            string birth = post["Birthday"];

            SqlConnection connStr = new SqlConnection(strConnString);
            string insertstring = @"INSERT INTO [dbo].[UserInfo] ([UserName],[Age],[birth]) VALUES(@username,@age,@birth)";
            SqlCommand cmd = new SqlCommand(insertstring, connStr);
            try 
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@birth", birth);
                connStr.Open();
                cmd.ExecuteNonQuery();
                connStr.Close();

                //using (SqlConnection conn = new SqlConnection(strConnString))
                //{
                //    string query = string.Format(@"select 
                //                          UserName,
                //                          Age,
                //                          birth
                //                   from [dbo].[UserInfo]");
                //    using (SqlCommand cmdforAll = new SqlCommand(query))
                //    {
                //        cmdforAll.Connection = conn;
                //        using (SqlDataAdapter sda = new SqlDataAdapter(cmdforAll))
                //        {
                //            sda.Fill(dt);
                //        }
                //    }
                //}
                //ViewData["UserInfo"] = dt;

            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error1", "Some error in DB INSERT!");
                //return View("../Shared/Error");
            }
            finally
            {
                cmd.Cancel();
                connStr.Close();
                connStr.Dispose();
            }

            return RedirectToAction("Demo");
        }
            public ActionResult Demo() 
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConnString)) 
            {
                string query = string.Format(@"select 
                                          UserName,
                                          Age,
                                          birth
                                   from [dbo].[UserInfo]");
                using (SqlCommand cmd = new SqlCommand(query)) 
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) 
                    {
                        sda.Fill(dt);
                    }
                }
            }
            ViewData["UserInfo"] = dt;
            return View();
        }
        //[HttpPost]
        public ActionResult Delete(string username)
        {
            DataTable dt = new DataTable();
            SqlConnection connStr = new SqlConnection(strConnString);
            string delete = string.Format
                (@"DELETE FROM [dbo].[UserInfo] WHERE UserName LIKE N'{0}'", username);
            SqlCommand cmd = new SqlCommand(delete, connStr);
            connStr.Open();
            cmd.ExecuteNonQuery();
            connStr.Close();
            //using (SqlConnection conn = new SqlConnection(strConnString))
            //{
            //    string query = string.Format(@"select 
            //                              UserName,
            //                              Age,
            //                              birth
            //                       from [dbo].[UserInfo]");
            //    using (SqlCommand cmdforAll = new SqlCommand(query))
            //    {
            //        cmdforAll.Connection = conn;
            //        using (SqlDataAdapter sda = new SqlDataAdapter(cmdforAll))
            //        {
            //            sda.Fill(dt);
            //        }
            //    }
            //}
            //ViewData["UserInfo"] = dt;
            return RedirectToAction("Demo");
        }
        public ActionResult Edit(string username)
        {
            User useredit = GetUserDetail(username);
            return View(useredit);

        }

        [HttpPost]
        public ActionResult Edit(User useredit)
        {
            Update(useredit);
            return RedirectToAction("Demo");

        }
        public void Update(User edituser)
        {
            
            SqlConnection connStr = new SqlConnection(strConnString);
            string birthday = string.Format("{0:yyyy-MM-dd HH:mm:ss}", edituser.Birthday);
            string update = string.Format
                (@"UPDATE [dbo].[UserInfo] SET UserName = N'{1}',Age='{2}',birth= CONVERT(datetime, '{3}')  WHERE Id = '{0}'", edituser.Id, edituser.UserName,edituser.Age, birthday);
            SqlCommand cmd = new SqlCommand(update, connStr);
            connStr.Open();
            cmd.ExecuteNonQuery();
            connStr.Close();
        }

        public User GetUserDetail(string username)
        {
            User useredit = new User();
            //string strConnString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            DataTable dt = new DataTable();
            SqlDataReader reader;
            SqlConnection connStr = new SqlConnection(strConnString);
           

            string find = string.Format
                (@"select Id,UserName, Age, birth from [dbo].[UserInfo] WHERE UserName LIKE N'{0}'", username);
            
            connStr.Open();
            SqlCommand cmd = new SqlCommand(find, connStr);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read()) { 
                    useredit = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        UserName = (String)reader["UserName"].ToString(),
                        Age = reader.GetInt32(reader.GetOrdinal("Age")) ,
                        Birthday = (DateTime)reader["birth"]
                    };
                }

            }
            connStr.Close();
            oldName = username;
            return useredit;
        }

        //public string[] GetData()
        //{
        //    string[] arr = { "", "", "" };
        //    using (SqlConnection conn = new SqlConnection(strConnString))
        //    {
        //        conn.Open();
        //        SqlCommand scom = new SqlCommand("", conn);
        //        scom.CommandText = @"   
        //                           select 
        //                                  UserName,
        //                                  Age,
        //                                  birth
        //                           from [dbo].[UserInfo]  ";

        //        SqlDataReader sread = scom.ExecuteReader();
        //        if (sread.Read())
        //        {
        //            arr[0] = sread["UserName"].ToString();
        //            arr[1] = sread["Age"].ToString();
        //            arr[2] = sread["birth"].ToString();

        //        }
        //    }
        //    return arr;
        //}
    }
}