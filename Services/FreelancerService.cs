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
    }
}
