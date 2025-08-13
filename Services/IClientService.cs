using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Services
{
    public interface IClientService
    {
        Task<bool> AddClient(Client client);
        Task<bool> AddTask(int clientId, ClientTask task);
    }
}
