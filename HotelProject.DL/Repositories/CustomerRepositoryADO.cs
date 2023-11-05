using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using HotelProject.DL.Exceptions;
using System;
using System.Collections.Generic;
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

        public List<Customer> GetCustomers(string filter)
        {
            try
            {
                Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
                string sql;

                if (string.IsNullOrEmpty(filter))
                {
                    sql = @"
                        SELECT
                            t1.id,
                            t1.email,
                            t1.name AS customername,
                            t1.address,
                            t1.phone,
                            t2.name AS membername,
                            t2.birthday
                        FROM
                            customer t1
                        LEFT JOIN
                            (SELECT * FROM member WHERE status = 1) t2
                        ON
                            t1.id = t2.id
                        WHERE
                            t1.status = 1";
                }

                else
                {
                    sql = @"
                        SELECT
                            t1.id,
                            t1.email,
                            t1.name AS customername,
                            t1.address,
                            t1.phone,
                            t2.name AS membername,
                            t2.birthday
                        FROM
                            customer t1
                        LEFT JOIN
                            (SELECT * FROM member WHERE status = 1) t2
                        ON
                            t1.id = t2.customerId
                        WHERE
                            t1.status = 1
                        AND
                            (t1.id LIKE @filter OR t1.name LIKE @filter OR t1.email LIKE @filter)";
                }

                using(SqlConnection conn = new SqlConnection(connectionString)) 
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
                    SqlDataReader reader=cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            int id= Convert.ToInt32(reader["ID"]);
                            if (!customers.ContainsKey(id)) //member toevoegen
                            {
                               customers.Add(id, new Customer((string)reader["customername"], (int)reader["id"], new ContactInfo((string)reader["email"], (string)reader["phone"], new Address((string)reader["address"]))));                              
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                            {
                                customers[id].AddMember(new Member((string)reader["membername"], DateOnly.FromDateTime((DateTime)reader["birthday"])));
                            }                            
                        }
                    return customers.Values.ToList();
                }
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("GetCustomer",ex);
            }
        }

        public void AddCustomer(Customer customer)
        {
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
                        int id=(int)cmd.ExecuteScalar();
                        //write members table
                        SQL = "INSERT INTO member(name,birthday,customerid,status) VALUES(@name,@birthday,@customerid,@status) ";
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
        }

        public int GetLastCustomerId()
        {
            int lastId = -1; // Default value if no rows are found.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = $"SELECT MAX(id) FROM Customer";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        lastId = Convert.ToInt32(result);
                    }
                }
            }

            return lastId;
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

        public void UpdateCustomer(Customer customer, int id)
        {
            string sql = "UPDATE Customer SET name = @Name, email = @Email, phone = @Phone, address = @Address WHERE ID = @CustomerId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                using (SqlCommand command = new SqlCommand(sql, connection, transaction))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@CustomerId", id);
                        command.Parameters.AddWithValue("@Name", customer.Name);
                        command.Parameters.AddWithValue("@Email", customer.ContactInfo.Email);
                        command.Parameters.AddWithValue("@Phone", customer.ContactInfo.Phone);
                        command.Parameters.AddWithValue("@Address", customer.ContactInfo.Address.ToAddressLine());

                        int rowsAffected = command.ExecuteNonQuery(); // Execute the SQL command to update the customer.

                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
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
    }
}
