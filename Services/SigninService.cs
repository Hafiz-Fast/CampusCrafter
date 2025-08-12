using Student_Freelance_Backend.Models;
using Student_Freelance_Backend.Repositories;
using System.Text.RegularExpressions;

namespace Student_Freelance_Backend.Services
{
    public class SigninService : ISigninService
    {
        private readonly ISigninRepo _repo;

        public SigninService(ISigninRepo repo)
        {
            _repo = repo;
        }

        public async Task<SigninResult> UserSignin(Signin signin)
        {
            // Email regex pattern (simple version)
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Password regex pattern (example: at least 8 chars, one uppercase, one lowercase, one number)
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

            if(!Regex.IsMatch(signin.email, emailPattern))
            {
                return new SigninResult
                {
                    flag = false,
                    Message = "Invalid Email Format"
                };
            }

            if(!Regex.IsMatch(signin.password, passwordPattern))
            {
                return new SigninResult
                {
                    flag = false,
                    Message = "Password must be at least 8 characters long, contain one uppercase, one lowercase, and one number"
                };
            }

            return await _repo.UserSignin(signin);
        }
    }
}
