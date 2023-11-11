using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class OrganizerRepositoryADO : IOrganizerRepository
    {
        private string connectionString;

        public OrganizerRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveOrganizer(Organizer newOrganizer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Insert the organizer details into the Organizer table
                string insertQuery = @"
                INSERT INTO Organizer (username, password_hash, name, email, phone, address)
                VALUES (@Username, @PasswordHash, @Name, @Email, @Phone, @Address);
            ";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Add parameters to the SQL command to prevent SQL injection
                    command.Parameters.AddWithValue("@Username", newOrganizer.Username);
                    command.Parameters.AddWithValue("@PasswordHash", newOrganizer.HashedPassword);
                    command.Parameters.AddWithValue("@Name", newOrganizer.Name);
                    command.Parameters.AddWithValue("@Email", newOrganizer.ContactInfo.Email);
                    command.Parameters.AddWithValue("@Phone", newOrganizer.ContactInfo.Phone);
                    command.Parameters.AddWithValue("@Address", newOrganizer.ContactInfo.Address.ToString());

                    // Execute the SQL command
                    command.ExecuteNonQuery();
                }
            }
        }

        public string GetHashedPasswordByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "SELECT password_hash FROM Organizer WHERE username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    // Execute the query
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if a user was found
                    if (reader.Read())
                    {
                        // Retrieve hashed_password and salt from the database
                        string hashedPassword = (string)reader["password_hash"];
                      



                        return hashedPassword;
                    }
                    else
                    {
                        // User not found
                        // Return some default values or throw an exception based on your error handling strategy
                        return null;
                    }
                }
            }
        }
    }
}
