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

        public async Task<bool> AddEducation(Education education)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("AddEducation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@freelancerId", education.freelancerId);
                cmd.Parameters.AddWithValue("@institute", education.institute);
                cmd.Parameters.AddWithValue("@degree", education.degree);
                cmd.Parameters.AddWithValue("@program", education.program);
                cmd.Parameters.Add("@startDate", SqlDbType.Date).Value = education.StartDate.Date;
                cmd.Parameters.Add("@endDate", SqlDbType.Date).Value = education.EndDate.Date;

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

        public async Task<bool> DeleteEducation(int EducationId)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("DeleteEducation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@educationId", EducationId);

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

        public async Task<bool> UpdateEducation(int EducationId,  Education education)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("UpdateEducation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@freelancerId", education.freelancerId);
                cmd.Parameters.AddWithValue("@educationId", EducationId);
                cmd.Parameters.AddWithValue("@institute", education.institute);
                cmd.Parameters.AddWithValue("@degree", education.degree);
                cmd.Parameters.AddWithValue("@program", education.program);
                cmd.Parameters.Add("@startDate", SqlDbType.Date).Value = education.StartDate.Date;
                cmd.Parameters.Add("@endDate", SqlDbType.Date).Value = education.EndDate.Date;

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

        public async Task<string> AddProposal(Proposal proposal)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("AddProposal", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@freelancerId", proposal.freelancerId);
                cmd.Parameters.AddWithValue("@taskId", proposal.taskId);
                cmd.Parameters.AddWithValue("@request", proposal.request);
                cmd.Parameters.AddWithValue("@links", string.Join(",", proposal.links));
                cmd.Parameters.AddWithValue("@bidAmount", proposal.bidAmount);

                SqlParameter FlagParam = new SqlParameter("@flag", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(FlagParam);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    int flag = (int)FlagParam.Value;
                    if(flag == -2)
                    {
                        return "Task does not exist";
                    }
                    else if(flag == 0)
                    {
                        return "Invalid Amount";
                    }
                    else if (flag == -1)
                    {
                        return "Less than 70% of budget";
                    }
                    else if (flag == 1)
                    {
                        return "More than 150% of budget";
                    }
                    else if (flag == 2)
                    {
                        return "Proposal Sent Successfully";
                    }
                }
                return "Task does not exist";
            }
        }

        public async Task<bool> DeleteProposal(int ProposalId)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("DeleteProposal", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@proposalId", ProposalId);

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

        public async Task<bool> AddPortfolio(Portfolio portfolio)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("AddPortfolio", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@freelancerId", portfolio.freelancerId);
                cmd.Parameters.AddWithValue("@imageURL", portfolio.ImageURL);
                cmd.Parameters.AddWithValue("@projectDescription", portfolio.ProjectDescription);
                cmd.Parameters.AddWithValue("@videoURL", portfolio.videoURL);

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

        public async Task<bool> DeletePortfolio(int PortfolioId)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("DeletePortfolio", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@portfolioId", PortfolioId);

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
        public async Task<bool> UpdatePortfolio(int PortfolioId, Portfolio portfolio)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            using (SqlCommand cmd = new SqlCommand("UpdatePortfolio", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@portfolioId", PortfolioId);
                cmd.Parameters.AddWithValue("@imageURL", portfolio.ImageURL);
                cmd.Parameters.AddWithValue("@projectDescription", portfolio.ProjectDescription);
                cmd.Parameters.AddWithValue("@videoURL", portfolio.videoURL);

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
    }
}
