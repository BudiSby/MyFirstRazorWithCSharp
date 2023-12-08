using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyFirstRazorWithCShrap.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMmessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0 )
            {
                errorMessage = "All the field are required";
                return;
            }

            //save data to database

            try 
            {
                String connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DB_Dummy1;Integrated Security=True;Trust Server Certificate=True";
                connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DB_Dummy1;Persist Security Info=True;User ID=Budiawan;Password=Budi9913!;";
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String sql = "Insert into clients (name,email,phone,address) Values (@name,@email,@phone,@address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("name", clientInfo.name);
                        command.Parameters.AddWithValue("email", clientInfo.email);
                        command.Parameters.AddWithValue("phone", clientInfo.phone);
                        command.Parameters.AddWithValue("address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex ) 
            {
                errorMessage= ex.Message;
                return;
            }  

            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            successMmessage = "New client added succesfully";

        }    
    }
}
