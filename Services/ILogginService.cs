using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Services
{
    public interface ILogginService
    {
        Task<LogginResult> LogginUser(string email, string password);
    }
}
