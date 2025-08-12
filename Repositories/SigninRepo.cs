using Microsoft.Data.SqlClient;
using System.Data;
using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Repositories
{
    public class SigninRepo : ISigninRepo
    {
        private readonly IConfiguration _config;
        public SigninRepo(IConfiguration config)
        {
            _config = config;
        }
        public async Task<SigninResult> UserSignin(Signin signin)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("Signin", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fname", signin.fname);
                cmd.Parameters.AddWithValue("@lname", signin.lname);
                cmd.Parameters.AddWithValue("@age", signin.age);
                cmd.Parameters.AddWithValue("@gender", signin.gender);
                cmd.Parameters.AddWithValue("@type", signin.type);
                cmd.Parameters.AddWithValue("@email", signin.email);
                cmd.Parameters.AddWithValue("@password", signin.password);

                SqlParameter IdParam = new SqlParameter("@userId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                var result = new SigninResult
                {
                    userId = (int)IdParam.Value
                };

                if(result.userId == -1)
                {
                    result.flag = false;
                    result.Message = "Error occurred while inserting in Database";
                }
                else
                {
                    result.flag = true;
                    result.Message = "Signin Successfully!";
                }

                return result;
            }
        }
    }
}
