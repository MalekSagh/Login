using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace Login.Controllers
{
    public class DeleteController : Controller
    {
        // GET: Delete
        public ActionResult Delete()
        {
            String id = Request.QueryString["id"];


            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();
                    String sql = "DELETE FROM clients WHERE id =@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
          return Redirect("/Client/Client");
          
        }
    }
}