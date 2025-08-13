using Student_Freelance_Backend.Models;

namespace Student_Freelance_Backend.Repositories
{
    public interface IClientRepo
    {
        Task<bool> AddClient(Client client);
        Task<bool> AddTask(int clientId, ClientTask task);
    }
}
