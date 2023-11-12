using System;
using System.Data.SqlClient;
using System.IO;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=LAPTOP-CRDVREFU\\SQLEXPRESS;Initial Catalog=VideoShoppingDB;Integrated Security=True;TrustServerCertificate=True;"
;
        string scriptFilePath = "C:\\Users\\lucac\\Downloads\\SolutionHotelWoensdag-master\\SolutionHotelWoensdag-master\\HotelProject.DL\\SQL\\CREATE_TABLES.sql"; 
        try
        {
            string sqlScript = File.ReadAllText(scriptFilePath);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlScript, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("SQL script executed successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing SQL script: {ex.Message}");
        }
    }
}
