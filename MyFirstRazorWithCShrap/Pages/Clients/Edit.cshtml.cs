using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyFirstRazorWithCShrap.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMmessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DB_Dummy1;Integrated Security=True;Trust Server Certificate=True";
                connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DB_Dummy1;Persist Security Info=True;User ID=Budiawan;Password=Budi9913!;";
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String sql = "select id,name,email,phone,address,CONVERT(varchar(25),created_at ,120) from clients where 1=1 and id=@id ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {                                
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetString(5).ToString();

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


        }

        public void OnPost() 
        {
            clientInfo.id = Request.Query["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
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
                    String sql = "Update clients Set name=@name ,email=@email, phone=@phone, address=@address where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();

                        Console.WriteLine(sql);
                        Console.WriteLine(clientInfo.id);

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            Response.Redirect("/Clients/Index");

        }    
    }
}
