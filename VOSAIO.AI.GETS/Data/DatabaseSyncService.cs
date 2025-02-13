using VOSAIO.AI.GETS.Data.Models;

namespace VOSAIO.AI.GETS.Data
{
    public class DatabaseSyncService: IDatabaseSyncService
    {
        private SqLiteDatabaseContext _sqLiteDatabaseContext;
        public DatabaseSyncService(SqLiteDatabaseContext sqLiteDatabaseContext) { _sqLiteDatabaseContext = sqLiteDatabaseContext; }

        public async Task<string> InsertUserRequest(UserRequest userRequest)
        {
            try
            {
                await _sqLiteDatabaseContext.UserRequests.AddAsync(userRequest);
                await _sqLiteDatabaseContext.SaveChangesAsync();
                return "success";
            }
            catch(Exception ex) { return $"Save Failed: {ex}"; }
        }
    }
}
