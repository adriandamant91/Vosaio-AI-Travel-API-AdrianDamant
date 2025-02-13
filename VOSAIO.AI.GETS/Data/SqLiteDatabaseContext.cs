using Microsoft.EntityFrameworkCore;
using VOSAIO.AI.GETS.Data.Models;

namespace VOSAIO.AI.GETS.Data
{
    public class SqLiteDatabaseContext : DbContext
    {
        public SqLiteDatabaseContext(DbContextOptions<SqLiteDatabaseContext> options) : base(options){}

        public DbSet<UserRequest> UserRequests { get; set; }
    }
}
