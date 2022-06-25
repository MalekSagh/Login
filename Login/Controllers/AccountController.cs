using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Login.Models;
using System.Configuration;

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
      
        [HttpPost]
        public ActionResult Login(Account acc)

        {
            try { 
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
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
            }catch(Exception ex)
            {
                errorMessage = ex.ToString();
                ViewBag.Message = errorMessage;
                return View();
            }
        }
    
    }
}