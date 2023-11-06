using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using HotelProject.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class ActivityRepositoryADO : IActivityRepository
    {
        private string connectionString;

        public ActivityRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Activity> GetActivities()
        {
            try
            {
                string sql;

                sql = @"
                   SELECT
                        A.ID AS ActivityID,
                        A.name AS ActivityName,
                        A.scheduled_date,
                        A.available_spots,
                        A.adult_price,
                        A.child_price,
                        A.discount,

                        AI.description AS ActivityDescription,
                        AI.address AS ActivityAddress,
                        AI.duration AS ActivityDuration
                    FROM
                        Activity A
                    INNER JOIN
                        Activity_Info AI
                    ON
                        A.activity_info_id = AI.ID;";



                List<Activity> activities = new List<Activity>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ActivityInfo activityInfo = new ActivityInfo(
                                (string)reader["ActivityDescription"],
                                new Address((string)reader["ActivityAddress"]),
                                (int)reader["ActivityDuration"]
                            );

                            Activity activity = new Activity(
                                (string)reader["ActivityName"],
                                activityInfo,
                                (DateOnly)reader["scheduled_date"],
                                (int)reader["available_spots"],
                                (decimal)reader["adult_price"],
                                (decimal)reader["child_price"],
                                (int)reader["discount"]
                            );

                            activities.Add(activity);
                        }
                    }
                }

                return activities;
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("GetCustomer", ex);
            }
        }

        public void AddActivity(Activity activity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert Activity Info and get the generated ID
                        int activityInfoID;
                        using (SqlCommand activityInfoCmd = new SqlCommand("INSERT INTO Activity_Info (description, address, duration) VALUES (@Description, @Address, @Duration); SELECT SCOPE_IDENTITY();", conn, transaction))
                        {
                            activityInfoCmd.Parameters.AddWithValue("@Description", activity.ActivityInfo.Desciption);
                            activityInfoCmd.Parameters.AddWithValue("@Address", activity.ActivityInfo.Address.ToAddressLine());
                            activityInfoCmd.Parameters.AddWithValue("@Duration", activity.ActivityInfo.Duration.ToString());

                            activityInfoID = Convert.ToInt32(activityInfoCmd.ExecuteScalar());
                        }

                        // Insert Activity with the generated ActivityInfo ID
                        using (SqlCommand activityCmd = new SqlCommand("INSERT INTO Activity (name, scheduled_date, available_spots, adult_price, child_price, discount, activity_info_id) VALUES (@Name, @ScheduledDate, @AvailableSpots, @AdultPrice, @ChildPrice, @Discount, @ActivityInfoID);", conn, transaction))
                        {
                            activityCmd.Parameters.AddWithValue("@Name", activity.Name);
                            activityCmd.Parameters.AddWithValue("@ScheduledDate", activity.ScheduledDate);
                            activityCmd.Parameters.AddWithValue("@AvailableSpots", activity.AvailableSpots);
                            activityCmd.Parameters.AddWithValue("@AdultPrice", activity.AdultPrice);
                            activityCmd.Parameters.AddWithValue("@ChildPrice", activity.ChildPrice);
                            activityCmd.Parameters.AddWithValue("@Discount", activity.Discount);
                            activityCmd.Parameters.AddWithValue("@ActivityInfoID", activityInfoID);

                            activityCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw; // Re-throw the exception for handling at a higher level.
                    }
                }
            }
        }


        public int GetLastActivityId()
        {
            int lastId = -1; // Default value if no rows are found.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = $"SELECT MAX(id) FROM Activity";

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

    }
}
