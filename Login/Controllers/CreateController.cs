using Login.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace Login.Controllers
{
    public class CreateController : Controller
    {
        public String errorMessage = "";
        // GET: Create
        public ActionResult Create()
        {
            ViewBag.Message = errorMessage;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients" +
                                "(name, email, phone, address) VALUES" +
                                "(@name, @email, @phone, @adress);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", client.Name);
                        command.Parameters.AddWithValue("@email", client.Email);
                        command.Parameters.AddWithValue("@phone", client.Phone);
                        command.Parameters.AddWithValue("@adress", client.Adress);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                ViewBag.Message = errorMessage;
                return View();
            }

            return RedirectToAction("../Client/Client");

        }
    }
}