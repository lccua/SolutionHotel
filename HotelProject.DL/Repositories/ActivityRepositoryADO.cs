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

        public List<Activity> GetActivities(string filter)
        {
            try
            {
                string sql;
                if (string.IsNullOrEmpty(filter))
                {
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
                }

                else
                {
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
                            A.activity_info_id = AI.ID
                        AND
                            (A.id LIKE @filter OR A.name LIKE @filter)";
                }

                List<Activity> activities = new List<Activity>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@filter", $"%{filter}%");

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
                                (string)reader["scheduled_date"],
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


        public int AddActivity(Activity activity)
        {
            int id = 0;

            try
            {
                string SQL = "INSERT INTO Activity_Info(description, address, duration) output INSERTED.ID VALUES(@Description,@Address,@Duration)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.CommandText = SQL;
                        cmd.Transaction = transaction;

                        cmd.Parameters.AddWithValue("@Description", activity.ActivityInfo.Desciption);
                        cmd.Parameters.AddWithValue("@Address", activity.ActivityInfo.Address.ToAddressLine());
                        cmd.Parameters.AddWithValue("@Duration", activity.ActivityInfo.Duration.ToString());

                        id = (int)cmd.ExecuteScalar();


                        SQL = "INSERT INTO Activity(name,scheduled_date,available_spots,adult_price,child_price,discount,activity_info_id) VALUES(@Name,@ScheduledDate,@AvailableSpots,@AdultPrice,@ChildPrice,@Discount,@ActivityInfoId) ";
                        cmd.CommandText = SQL;

                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@Name", activity.Name);
                        cmd.Parameters.AddWithValue("@ScheduledDate", activity.ScheduledDate);
                        cmd.Parameters.AddWithValue("@AvailableSpots", activity.AvailableSpots);
                        cmd.Parameters.AddWithValue("@AdultPrice", activity.AdultPrice);
                        cmd.Parameters.AddWithValue("@ChildPrice", activity.ChildPrice);
                        cmd.Parameters.AddWithValue("@Discount", activity.Discount);
                        cmd.Parameters.AddWithValue("@ActivityInfoID", id);

                        cmd.ExecuteNonQuery();

                        transaction.Commit();

                       
                     
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("AddCustomer", ex);
            }
            return id;
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
