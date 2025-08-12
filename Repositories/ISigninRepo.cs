using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Repositories
{
    public interface ISigninRepo
    {
        Task<SigninResult> UserSignin(Signin signin);
    }
}
