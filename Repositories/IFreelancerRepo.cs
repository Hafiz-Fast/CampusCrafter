using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Repositories
{
    public interface IFreelancerRepo
    {
        Task<bool> AddFreelancer(Freelancer freelancer);

        Task<bool> AddEducation(Education education);
    }
}
