using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Repositories
{
    public interface IFreelancerRepo
    {
        Task<bool> AddFreelancer(Freelancer freelancer);
        Task<bool> AddEducation(Education education);
        Task<bool> DeleteEducation(int EducationId);
        Task<bool> UpdateEducation(int EducationId, Education education);
        Task<string> AddProposal(Proposal proposal);
        Task<bool> DeleteProposal(int ProposalId);
        Task<bool> AddPortfolio(Portfolio portfolio);
        Task<bool> DeletePortfolio(int portfolioId);
        Task<bool> UpdatePortfolio(int portfolioId, Portfolio portfolio);
    }
}
