using Microsoft.AspNetCore.Mvc;
using Student_Freelance_Backend.Models;
using Student_Freelance_Backend.Services;

namespace Student_Freelance_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreelancerController : Controller
    {
        private readonly IFreelancerService _service;

        public FreelancerController(IFreelancerService service)
        {
            _service = service;
        }

        [HttpPost("AddFreelancer")]
        public async Task<IActionResult> AddFreelancer(Freelancer freelancer)
        {
            var result = await _service.AddFreelancer(freelancer);

            return Ok(result);
        }

        [HttpPost("AddEducation")]
        public async Task<IActionResult> AddEducation(Education education)
        {
            var result = await _service.AddEducation(education);

            return Ok(result);
        }
    }
}
