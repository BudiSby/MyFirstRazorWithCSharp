using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

//https://www.youtube.com/watch?v=T-e554Zt3n4

namespace MyFirstRazorWithCShrap.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
               
                String connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DB_Dummy1;Integrated Security=True;Trust Server Certificate=True";
                connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DB_Dummy1;Persist Security Info=True;User ID=Budiawan;Password=Budi9913!;";
                using (SqlConnection connection = new SqlConnection(connString)) 
                { 
                    connection.Open();
                    String sql = "select id,name,email,phone,address,CONVERT(varchar(25),created_at ,120) from clients where 1=1 ";
                    using (SqlCommand command = new SqlCommand(sql,connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                        while (reader.Read()) 
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2); 
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetString(5).ToString();

                                listClients.Add(clientInfo);
                            }   
                        }  
                    }
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception : " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }

}
