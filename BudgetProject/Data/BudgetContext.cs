using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
