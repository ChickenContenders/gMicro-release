// This class may be unnecessary
using Micro;

namespace GameMicroServer.Services
{
    public interface IGameRepository
    {
        Task<ICollection<GameInfo>> ReadAllAsync();
        Task<GameInfo> GetByIdAsync(int id);
    }
}
