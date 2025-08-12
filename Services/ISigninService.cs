using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Services
{
    public interface ISigninService
    {
        Task<SigninResult> UserSignin(Signin signin);
    }
}
