using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class FinanceContext:DbContext
    {
        public FinanceContext(DbContextOptions<FinanceContext> options):base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Account> accounts { get; set; }
        public DbSet<Transaction> transactions { get; set; }
    }
}