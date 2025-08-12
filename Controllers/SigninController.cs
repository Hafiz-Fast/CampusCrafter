using Microsoft.AspNetCore.Mvc;
using Student_Freelance_Backend.Models;
using Student_Freelance_Backend.Services;

namespace Student_Freelance_Backend.Controllers
{ 

    [ApiController]
    [Route("api/[controller]")]
    public class SigninController : Controller
    {
        private readonly ISigninService _service;

        public SigninController(ISigninService service)
        {
            _service = service;
        }

        [HttpPost("UserSignin")]
        public async Task<IActionResult> UserSignin(Signin user)
        {
            var result = await _service.UserSignin(user);
            
            if(result.userId != -1)
            {
                TempData["SigninId"] = result.userId;
            }

            return Ok(result);
        }
    }
}
