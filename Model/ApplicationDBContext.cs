using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Model
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
