using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class Edit1Model : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
            string ID = Request.Query["ID"];
            try
            {
                String connectionString = "Data Source=SALMAN\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Clients WHERE ID = @ID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                clientInfo.ID = "" + reader.GetInt32(0);
                                clientInfo.NAME = reader.GetString(1);
                                clientInfo.EMAIL = reader.GetString(2);
                                clientInfo.PHONE = reader.GetString(3);
                                clientInfo.ADDRESS = reader.GetString(4);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;

            }
        }
        public void OnPost()
        {
            clientInfo.ID = Request.Form["ID"];
            clientInfo.NAME = Request.Form["NAME"];
            clientInfo.EMAIL = Request.Form["EMAIL"];
            clientInfo.PHONE = Request.Form["PHONE"];
            clientInfo.ADDRESS = Request.Form["ADDRESS"];

            if (clientInfo.NAME.Length == 0 || clientInfo.EMAIL.Length == 0 ||
                clientInfo.PHONE.Length == 0 || clientInfo.ADDRESS.Length == 0)
            {
                ErrorMessage = "All Fields Required";
                return;
            }

            try
            {
                String connectionString = "Data Source=SALMAN\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Clients " +
                                "SET NAME = @NAME,EMAIL = @EMAIL, PHONE = @PHONE ,ADDRESS = @ADDRESS " +
                                " WHERE ID = @ID;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@NAME", clientInfo.NAME);
                        command.Parameters.AddWithValue("@EMAIL", clientInfo.EMAIL);
                        command.Parameters.AddWithValue("@PHONE", clientInfo.PHONE);
                        command.Parameters.AddWithValue("@ADDRESS", clientInfo.ADDRESS);
                        command.Parameters.AddWithValue("@ID", clientInfo.ID);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;

            }

            Response.Redirect("/Clients/Index");
        }

    }
}