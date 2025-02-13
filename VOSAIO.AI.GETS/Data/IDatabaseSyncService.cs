using VOSAIO.AI.GETS.Data.Models;

namespace VOSAIO.AI.GETS.Data
{
    public interface IDatabaseSyncService
    {
        Task<string> InsertUserRequest(UserRequest userRequest);
    }
}
