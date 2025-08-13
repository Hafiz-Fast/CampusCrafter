using Student_Freelance_Backend.Models;
using Student_Freelance_Backend.Repositories;

namespace Student_Freelance_Backend.Services
{
    public class FreelancerService : IFreelancerService
    {
        private readonly IFreelancerRepo _repo;

        public FreelancerService(IFreelancerRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddFreelancer(Freelancer freelancer)
        {
            if(freelancer.domain.Length > 3)
            {
                return false;
            }

            if(freelancer.ProfficientSkills.Length > 5)
            {
                return false;
            }

            if(freelancer.IntermediateSkills.Length > 5)
            {
                return false;
            }

            if(freelancer.FamiliarSkills.Length > 5)
            {
                return false;
            }

            return await _repo.AddFreelancer(freelancer);
        }

        public async Task<bool> AddEducation(Education education)
        {
            return await _repo.AddEducation(education);
        }
        public async Task<bool> DeleteEducation(int EducationId)
        {
            if(EducationId <= 0)
            {
                return false;
            }

            return await _repo.DeleteEducation(EducationId);
        }
        public async Task<bool> UpdateEducation(int EducationId,  Education education)
        {
            if(EducationId <= 0 || education.freelancerId <= 0)
            {
                return false;
            }

            return await _repo.UpdateEducation(EducationId, education);
        }
        public async Task<string> AddProposal(Proposal proposal)
        {
            if(proposal.freelancerId <= 0 || proposal.taskId <= 0)
            {
                return "Task does not exist";
            }

            return await _repo.AddProposal(proposal);
        }
        public async Task<bool> DeleteProposal(int proposalId)
        {
            if(proposalId <= 0)
            {
                return false;
            }

            return await _repo.DeleteProposal(proposalId);
        }
        public async Task<bool> AddPortfolio(Portfolio portfolio)
        {
            if(portfolio.freelancerId <= 0)
            {
                return false;
            }

            return await _repo.AddPortfolio(portfolio);
        }
        public async Task<bool> DeletePortfolio(int portfolioId)
        {
            if(portfolioId <= 0)
            {
                return false;
            }

            return await _repo.DeletePortfolio(portfolioId);
        }
        public async Task<bool> UpdatePortfolio(int portfolioId, Portfolio portfolio)
        {
            if(portfolioId <= 0 || portfolio.freelancerId <= 0)
            {
                return false;
            }

            return await _repo.UpdatePortfolio(portfolioId, portfolio);
        }
    }
}
