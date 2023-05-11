using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplicationMVC.Models
{

    public class User
    {
        public int Id { get; set; }
        [Required]
        //[Display(Name = "Name", ResourceType = typeof(MTI_Leave.Resources.GlobalRes))]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        //[Display(Name = "EmployeeID", ResourceType = typeof(MTI_Leave.Resources.GlobalRes))]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy/M/d HH:mm}", ApplyFormatInEditMode = true)]
        //[Display(Name = "StartTime", ResourceType = typeof(MTI_Leave.Resources.GlobalRes))]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }
    }
    //public User GetUserDetail(string username)
    //{
    //    User useredit = new User();
    //    string strConnString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
    //    DataTable dt = new DataTable();
    //    SqlDataReader reader;
    //    SqlConnection connStr = new SqlConnection(strConnString);
    //    var drusername = "";
    //    var drage = "";
    //    var drbirth = "";

    //    string find = string.Format
    //        (@"select UserName, Age, birth from [dbo].[UserInfo] WHERE UserName LIKE N'{0}'", username);
    //    SqlCommand cmd = new SqlCommand(find, connStr);
    //    connStr.Open();
    //    reader = cmd.ExecuteReader();
    //    if (reader.HasRows)
    //    {
    //        useredit = new User
    //        {
    //            UserName = reader.GetString(reader.GetOrdinal("UserName")),
    //            Age = reader.GetInt32(reader.GetOrdinal("Age")),
    //            Birthday = (DateTime)reader["birth"]
    //        };
            
    //    }
    //    connStr.Close();
    //    return useredit;
    //}
}