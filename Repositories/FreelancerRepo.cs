using Microsoft.Data.SqlClient;
using System.Data;
using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Repositories
{
    public class FreelancerRepo : IFreelancerRepo
    {
        private readonly IConfiguration _config;

        public FreelancerRepo(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> AddFreelancer(Freelancer freelancer)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("FreelancerDetails", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@freelancerId", freelancer.FreelancerId);
                cmd.Parameters.AddWithValue("@domain", string.Join(",", freelancer.domain));
                cmd.Parameters.AddWithValue("@summary", freelancer.summary);
                cmd.Parameters.AddWithValue("@profficientSkills", string.Join(",", freelancer.ProfficientSkills));
                cmd.Parameters.AddWithValue("@intermediateSkills", string.Join(",", freelancer.IntermediateSkills));
                cmd.Parameters.AddWithValue("@familiarSkills", string.Join(",", freelancer.FamiliarSkills));

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                if(rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
