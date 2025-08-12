using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Services
{
    public interface IFreelancerService
    {
        Task<bool> AddFreelancer(Freelancer freelancer);
    }
}
