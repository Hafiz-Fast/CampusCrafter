using Student_Freelance_Backend.Models;
using Student_Freelance_Backend.Repositories;
using System.Text.RegularExpressions;

namespace Student_Freelance_Backend.Services
{
    public class LogginService : ILogginService
    {
        private readonly ILogginRepo _repo;

        public LogginService(ILogginRepo repo)
        {
            _repo = repo;
        }

        public async Task<LogginResult> LogginUser(string email, string password)
        {
            // Email regex pattern (simple version)
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Password regex pattern (example: at least 8 chars, one uppercase, one lowercase, one number)
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

            if(!Regex.IsMatch(email, emailPattern))
            {
                return new LogginResult
                {
                    Success = false,
                    message = "Invalid Email Format"
                };
            }

            if(!Regex.IsMatch(password, passwordPattern))
            {
                return new LogginResult
                {
                    Success = false,
                    message = "Password must be at least 8 characters long, contain one uppercase, one lowercase, and one number"
                };
            }

            return await _repo.LogginUser(email, password);
        }
    }
}
