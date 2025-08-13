using Microsoft.AspNetCore.Mvc;
using Student_Freelance_Backend.Models;
using Student_Freelance_Backend.Services;

namespace Student_Freelance_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

        [HttpPost("AddClientDetails")]
        public async Task<IActionResult> AddClient(Client client)
        {
            var result = await _service.AddClient(client);

            return Ok(result);
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask(int clientId, ClientTask clientTask)
        {
            var result = await _service.AddTask(clientId, clientTask);

            return Ok(result);
        }
    }
}
