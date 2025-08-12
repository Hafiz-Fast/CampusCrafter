using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using Student_Freelance_Backend.Services;

namespace Student_Freelance_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogginController : Controller
    {
        private readonly ILogginService _service;
        public LogginController(ILogginService service)
        {
            _service = service;
        }

        [HttpPost("LogginUser")]
        public async Task<IActionResult> LogginUser(string email, string password)
        {
            var result = await _service.LogginUser(email, password);
            return Ok(result);
        }
    }
}
