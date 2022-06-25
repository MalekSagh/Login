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
    public class EditController : Controller
    {
        public Client client = new Client();
        public String errorMessage = "";
        public String successMessage = "";
        public ActionResult Edit()
        {
            OnGet();
            ViewBag.Message = errorMessage;
            return View(client);
        }

        [HttpPost]
        public ActionResult Edit(Client client)
        {
            OnPost(client);
            return Redirect("/Clients/Index");
        }
        public void OnGet()
        {
            String id = Request.QueryString["id"];
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id =@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                client.Id = "" + reader.GetInt32(0);
                                client.Name = reader.GetString(1);
                                client.Email = reader.GetString(2);
                                client.Phone = reader.GetString(3);
                                client.Adress = reader.GetString(4);

                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

        }

        public void OnPost(Client client)
        {
         

            if (client.Id.Length == 0 || client.Name.Length == 0 ||
                client.Email.Length == 0 || client.Phone.Length == 0 ||
                client.Adress.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                                 "SET name=@name, phone=@phone, address=@adress " +
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", client.Name);
                        //command.Parameters.AddWithValue("@email", client.Email);
                        command.Parameters.AddWithValue("@phone", client.Phone);
                        command.Parameters.AddWithValue("@adress", client.Adress);
                        command.Parameters.AddWithValue("@id", client.Id);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return;
            }

           
        }


    }
}
