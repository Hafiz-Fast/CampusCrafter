using Student_Freelance_Backend.Models;
using Student_Freelance_Backend.Repositories;

namespace Student_Freelance_Backend.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepo _repo;

        public ClientService(IClientRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddClient(Client client)
        {
            if(client.clientId <= 0)
            {
                return false;
            }

            return await _repo.AddClient(client);
        }

        public async Task<bool> AddTask(int clientId, ClientTask task)
        {
            if(clientId <= 0)
            {
                return false;
            }

            return await _repo.AddTask(clientId, task);
        }
    }
}
