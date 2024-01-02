using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class Create1Model : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
        }

              public void OnPost()
            {
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
                        string sql = "INSERT INTO Clients" +
                                      "(NAME,EMAIL,PHONE,ADDRESS) VALUES " +
                                      "(@NAME,@EMAIL,@PHONE,@ADDRESS);";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@NAME", clientInfo.NAME);
                            command.Parameters.AddWithValue("@EMAIL", clientInfo.EMAIL);
                            command.Parameters.AddWithValue("@PHONE", clientInfo.PHONE);
                            command.Parameters.AddWithValue("@ADDRESS", clientInfo.ADDRESS);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {

                    ErrorMessage = ex.Message;
                    return;
                }
                //save the data to the data base

                clientInfo.NAME = ""; clientInfo.EMAIL = ""; clientInfo.PHONE = ""; clientInfo.ADDRESS = "";
                SuccessMessage = "New Client Added Successfully";

                Response.Redirect("/Clients/Index");
            }
        
    }
}
