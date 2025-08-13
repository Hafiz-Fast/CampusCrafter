using Microsoft.Data.SqlClient;
using System.Data;
using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Repositories
{
    public class ClientRepo : IClientRepo
    {
        private readonly IConfiguration _config;

        public ClientRepo(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> AddClient(Client client)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("ClientDetails", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@clientId", client.clientId);
                cmd.Parameters.AddWithValue("@domain", string.Join(",", client.domain));
                cmd.Parameters.AddWithValue("@requiredSkills", string.Join(",", client.requiredSkills));

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<bool> AddTask(int clientId, ClientTask task)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("AddTasks", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@clientId", clientId);
                cmd.Parameters.AddWithValue("@taskType", task.taskType);
                cmd.Parameters.AddWithValue("@taskTitle", task.taskTitle);
                cmd.Parameters.AddWithValue("@taskDescription", task.taskDescription);
                cmd.Parameters.AddWithValue("@taskRequirements", task.taskRequirements);
                cmd.Parameters.AddWithValue("@taskDeadline", task.taskDeadline);
                cmd.Parameters.AddWithValue("@taskBudget", task.taskBudget);

                SqlParameter FlagParam = new SqlParameter("@flag", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(FlagParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                int flag = (int)FlagParam.Value;

                if(flag == 1)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
