using Microsoft.EntityFrameworkCore;
using BudgetProject.Models;

namespace BudgetAPI.Data
{
    public class BudgetContext : DbContext
    {
        public BudgetContext (DbContextOptions<BudgetContext> options)
            : base(options)
        {
        }

        public DbSet<Income> Income { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<Outcome> Outcome { get; set; }
    }
}
