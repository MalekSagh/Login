using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Login.Models;

namespace Login.Controllers
{
    public class AccountController : Controller
    {
       
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        public String errorMessage = "";

        
        public ActionResult Login()
        {
            ViewBag.Message = errorMessage;
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
        }

        [HttpPost]
        public ActionResult Login(Account acc)

        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM login WHERE username='" + acc.Name + "' and password='" + acc.Password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return RedirectToAction("../Client/Client");
            }
            else
            {
                //con.Close();
                errorMessage = "User name or Password is incorrect !";
                ViewBag.Message = errorMessage;
                return View();
              
            }
        }
    
    }
}