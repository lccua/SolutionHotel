using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using HotelProject.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class CustomerRepositoryADO : ICustomerRepository
    {
        private string connectionString;

        public CustomerRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Customer> GetCustomers(string searchFilter)
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Retrieve customer information with an optional search filter
                string customerQuery = "SELECT * FROM Customer t1 WHERE t1.status = 1"; // Always true
                if (!string.IsNullOrEmpty(searchFilter))
                {
                    customerQuery += " AND (t1.ID LIKE @filter OR t1.name LIKE @filter OR t1.email LIKE @filter)";
                }

                using (SqlCommand customerCommand = new SqlCommand(customerQuery, connection))
                {
                    if (!string.IsNullOrEmpty(searchFilter))
                    {
                        customerCommand.Parameters.AddWithValue("@filter", "%" + searchFilter + "%");
                    }

                    using (SqlDataReader customerReader = customerCommand.ExecuteReader())
                    {
                        while (customerReader.Read())
                        {
                            Customer customer = new Customer((string)customerReader["name"], (int)customerReader["id"], 
                                                new ContactInfo((string)customerReader["email"], (string)customerReader["phone"], 
                                                new Address((string)customerReader["address"])));


                            // Populate other properties of the Customer object
                            customers.Add(customer);
                        }
                    }
                }

                // Retrieve members associated with each customer
                foreach (Customer customer in customers)
                {
                    string memberQuery = "SELECT * FROM Member WHERE customer_id = @CustomerId AND status = 1";
                    using (SqlCommand memberCommand = new SqlCommand(memberQuery, connection))
                    {
                        memberCommand.Parameters.AddWithValue("@CustomerId", customer.Id);

                        using (SqlDataReader memberReader = memberCommand.ExecuteReader())
                        {
                            while (memberReader.Read())
                            {
                                Member member = new Member((int)memberReader["id"],(string)memberReader["name"], DateOnly.FromDateTime((DateTime)memberReader["birthday"]));

                                // Populate other properties of the Member object
                                customer.AddMember(member);
                            }
                        }
                    }
                }
            }

            return customers;
        }

        public int AddCustomer(Customer customer)
        {
            int id = 0;
            try
            {
                string SQL = "INSERT INTO Customer(name,email,phone,address,status) output INSERTED.ID VALUES(@name,@email,@phone,@address,@status) ";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction=conn.BeginTransaction();
                    try
                    {
                        //write customer table
                        cmd.CommandText = SQL;
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@name", customer.Name);
                        cmd.Parameters.AddWithValue("@email", customer.ContactInfo.Email);
                        cmd.Parameters.AddWithValue("@phone", customer.ContactInfo.Phone);
                        cmd.Parameters.AddWithValue("@address", customer.ContactInfo.Address.ToAddressLine());
                        cmd.Parameters.AddWithValue("@status", 1);
                        id=(int)cmd.ExecuteScalar();


                        //write members table
                        SQL = "INSERT INTO member(name,birthday,customer_id,status) VALUES(@name,@birthday,@customerid,@status) ";
                        cmd.CommandText=SQL;
                        
                        foreach(Member member in customer.GetMembers())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@name",member.Name);
                            cmd.Parameters.AddWithValue("@birthday", member.BirthDay.ToDateTime(TimeOnly.MinValue));
                            cmd.Parameters.AddWithValue("@customerid", id);
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("AddCustomer", ex);
            }
            return id;
        }

        public void DeleteCustomer(int customerId)
        {
            string sql = "UPDATE Customer SET status = 0 WHERE ID = @CustomerId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                using (SqlCommand command = new SqlCommand(sql, connection, transaction))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@CustomerId", customerId);
                        int rowsAffected = command.ExecuteNonQuery(); // Execute the SQL command to update the status.

                        if (rowsAffected > 0)
                        {
                            transaction.Commit(); // Commit the transaction if the update was successful.
                        }
                        else
                        {
                            transaction.Rollback(); // Rollback the transaction if no rows were updated (customer not found).
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // Rollback the transaction if an exception occurs.
                        throw; // Re-throw the exception for further handling.
                    }
                }
            }
        }

        public void DeleteMember(int memberId)
        {
            string sql = "UPDATE Member SET status = 0 WHERE ID = @MemberId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                using (SqlCommand command = new SqlCommand(sql, connection, transaction))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@MemberId", memberId);
                        int rowsAffected = command.ExecuteNonQuery(); // Execute the SQL command to update the status.

                        if (rowsAffected > 0)
                        {
                            transaction.Commit(); // Commit the transaction if the update was successful.
                        }
                        else
                        {
                            transaction.Rollback(); // Rollback the transaction if no rows were updated (customer not found).
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // Rollback the transaction if an exception occurs.
                        throw; // Re-throw the exception for further handling.
                    }
                }
            }
        }

        public void UpdateCustomer(Customer customer, int id)
        {
            string updateSQL = "UPDATE Customer SET name = @Name, email = @Email, phone = @Phone, address = @Address WHERE ID = @CustomerId";
            string insertSQL = "INSERT INTO member(name,birthday,customer_id,status) VALUES(@name,@birthday,@customerid,@status)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update operation
                        using (SqlCommand updateCommand = new SqlCommand(updateSQL, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@CustomerId", id);
                            updateCommand.Parameters.AddWithValue("@Name", customer.Name);
                            updateCommand.Parameters.AddWithValue("@Email", customer.ContactInfo.Email);
                            updateCommand.Parameters.AddWithValue("@Phone", customer.ContactInfo.Phone);
                            updateCommand.Parameters.AddWithValue("@Address", customer.ContactInfo.Address.ToAddressLine());

                            updateCommand.ExecuteNonQuery();
                        }

                        // Insert operation
                        using (SqlCommand insertCommand = new SqlCommand(insertSQL, connection, transaction))
                        {
                            foreach (Member member in customer.GetMembers())
                            {
                                insertCommand.Parameters.Clear();
                                insertCommand.Parameters.AddWithValue("@name", member.Name);
                                insertCommand.Parameters.AddWithValue("@birthday", member.BirthDay.ToDateTime(TimeOnly.MinValue));
                                insertCommand.Parameters.AddWithValue("@customerid", id);
                                insertCommand.Parameters.AddWithValue("@status", 1);
                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit(); // Commit the transaction if both update and insert operations succeed
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // Rollback the transaction if an exception occurs.
                        throw; // Re-throw the exception for further handling.
                    }
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
