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

        [HttpDelete("DeleteEducation")]
        public async Task<IActionResult> DeleteEducation(int EducationId)
        {
            var result = await _service.DeleteEducation(EducationId);

            return Ok(result);
        }

        [HttpPut("UpdateEducation")]
        public async Task<IActionResult> UpdateEducation(int EducationId, Education education)
        {
            var result = await _service.UpdateEducation(EducationId, education);

            return Ok(result);
        }

        [HttpPost("AddProposal")]
        public async Task<IActionResult> AddProposal(Proposal proposal)
        {
            var result = await _service.AddProposal(proposal);

            return Ok(result);
        }

        [HttpDelete("DeleteProposal")]
        public async Task<IActionResult> DeleteProposal(int proposalId)
        {
            var result = await _service.DeleteProposal(proposalId);

            return Ok(result);
        }

        [HttpPost("AddPortfolio")]
        public async Task<IActionResult> AddPortfolio(Portfolio portfolio)
        {
            var result = await _service.AddPortfolio(portfolio);

            return Ok(result);
        }

        [HttpDelete("DeletePortfolio")]
        public async Task<IActionResult> DeletePortfolio(int portfolioId)
        {
            var result = await _service.DeletePortfolio(portfolioId);

            return Ok(result);
        }

        [HttpPut("UpdatePortfolio")]
        public async Task<IActionResult> UpdatePortfolio(int portfolioId, Portfolio portfolio)
        {
            var result = await _service.UpdatePortfolio(portfolioId, portfolio);

            return Ok(result);
        }
    }
}
