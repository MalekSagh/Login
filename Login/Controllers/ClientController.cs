using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.SqlClient;
using Login.Models;

namespace Login.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public List<Client> listClients = new List<Client>();
        public String errorMessage = "";
        public ActionResult Client()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Client clientInfo = new Client();
                                clientInfo.Id = "" + reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Email = reader.GetString(2);
                                clientInfo.Phone = reader.GetString(3);
                                clientInfo.Adress = reader.GetString(4);
                                clientInfo.Created_at = reader.GetDateTime(5).ToString();
                                listClients.Add(clientInfo);
                            }
                        }
                        connection.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                ViewBag.Message = errorMessage;
                return View();
            }

            ViewBag.Message = errorMessage;
            ViewBag.List = listClients;
            ViewData["listClients"] = listClients;
            return View(listClients);
        }

    }
}