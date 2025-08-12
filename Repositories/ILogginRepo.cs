using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Repositories
{
    public interface ILogginRepo
    {
        Task<LogginResult> LogginUser(string email, string password);
    }
}
