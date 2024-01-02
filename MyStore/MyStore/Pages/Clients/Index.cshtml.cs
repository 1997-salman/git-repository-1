using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> ListClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=SALMAN\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Clients";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.ID = "" + reader.GetInt32(0);
                                clientInfo.NAME = reader.GetString(1);
                                clientInfo.EMAIL = reader.GetString(2);
                                clientInfo.PHONE = reader.GetString(3);
                                clientInfo.ADDRESS = reader.GetString(4);
                                clientInfo.CREATED_AT = reader.GetDateTime(5).ToString();

                                ListClients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public string ID;
        public string NAME;
        public string EMAIL;
        public string PHONE;
        public string ADDRESS;
        public string CREATED_AT;   

    }
}
