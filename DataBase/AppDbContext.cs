using Microsoft.EntityFrameworkCore;
using test_task.Models;

namespace test_task.DataBase
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<HelpdeskRequest> HelpdeskRequests { get; set; }
    }
}
