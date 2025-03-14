using Microsoft.EntityFrameworkCore;
using TicTacToe.Services;

namespace TicTacToe
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}