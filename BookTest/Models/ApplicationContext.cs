using Microsoft.EntityFrameworkCore;

namespace BookTest.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
