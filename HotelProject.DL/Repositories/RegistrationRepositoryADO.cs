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
    public class RegistrationRepositoryADO : IRegistrationRepository
    {
        private string connectionString;

        public RegistrationRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            return totalPrice;
        }

        public void SaveRegistration(Registration registration)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Insert registration into Registration table
                string insertRegistrationQuery = "INSERT INTO Registration (total_price, customer_id, activity_id) OUTPUT INSERTED.ID " +
                                                 "VALUES (@totalPrice, @customerId, @activityId)";

                SqlCommand registrationCommand = new SqlCommand(insertRegistrationQuery, connection);
                registrationCommand.Parameters.AddWithValue("@totalPrice", registration.TotalPrice);
                registrationCommand.Parameters.AddWithValue("@customerId", registration.Customer.Id);
                registrationCommand.Parameters.AddWithValue("@activityId", registration.Activity.Id);

                int registrationId = (int)registrationCommand.ExecuteScalar();

                // Insert members into RegistrationMember table
                string insertMemberQuery = "INSERT INTO Registration_Member (registration_id, member_id) " +
                                           "VALUES (@registrationId, @memberId)";

                SqlCommand memberCommand = new SqlCommand(insertMemberQuery, connection);

                foreach (Member member in registration.Members)
                {
                    memberCommand.Parameters.Clear();
                    memberCommand.Parameters.AddWithValue("@registrationId", registrationId);
                    memberCommand.Parameters.AddWithValue("@memberId", member.Id);

                    memberCommand.ExecuteNonQuery();
                }
            }
        }

    }
}
