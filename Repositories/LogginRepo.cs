using Microsoft.Data.SqlClient;
using System.Data;
using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Repositories
{
    public class LogginRepo : ILogginRepo
    {
        private readonly IConfiguration _config;

        public LogginRepo(IConfiguration config)
        {
            _config = config;
        }

        public async Task<LogginResult> LogginUser(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("LogginUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                SqlParameter IdParam = new SqlParameter("@userId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdParam);

                SqlParameter TypeParam = new SqlParameter("@userType", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(TypeParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return new LogginResult
                {
                    UserId = (int)IdParam.Value,
                    UserType = (string)TypeParam.Value,
                };
            }
        }
    }
}
