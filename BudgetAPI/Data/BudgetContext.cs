using Microsoft.EntityFrameworkCore;
using BudgetProject.Models;
using BudgetAPI.Models.DbEntities;
using BudgetProject.Models.DbEntities;

namespace BudgetAPI.Data
{
    public class BudgetContext : DbContext
    {
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Outcome> Outcomes { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClientSource> ClientSources { get; set; }
        public DbSet<ClientCategory> ClientCategories { get; set; }
        public DbSet<ClientItem> ClientItems { get; set; }

        public BudgetContext (DbContextOptions<BudgetContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many-to-many: Client <--> Category
            modelBuilder.Entity<ClientCategory>()
                .HasKey(cc => new { cc.ClientId, cc.CategoryId });

            modelBuilder.Entity<ClientCategory>()
                .HasOne(cc => cc.Category)
                .WithMany(c => c.ClientCategories)
                .HasForeignKey(cc => cc.CategoryId);

            modelBuilder.Entity<ClientCategory>()
                .HasOne(cc => cc.Client)
                .WithMany(c => c.ClientCategories)
                .HasForeignKey(cc => cc.ClientId);

            //many-to-many: Client <--> Item
            modelBuilder.Entity<ClientItem>()
               .HasKey(ci => new { ci.ClientId, ci.ItemId });

            modelBuilder.Entity<ClientItem>()
                .HasOne(ci => ci.Client)
                .WithMany(c => c.ClientItems)
                .HasForeignKey(ci => ci.ClientId);

            modelBuilder.Entity<ClientItem>()
                .HasOne(ci => ci.Item)
                .WithMany(c => c.ClientItems)
                .HasForeignKey(ci => ci.ItemId);

            //many-to-many: Client <--> Source
            modelBuilder.Entity<ClientSource>()
               .HasKey(cs => new { cs.ClientId, cs.SourceId });

            modelBuilder.Entity<ClientSource>()
                .HasOne(cs => cs.Client)
                .WithMany(c => c.ClientSources)
                .HasForeignKey(cs => cs.ClientId);

            modelBuilder.Entity<ClientSource>()
                .HasOne(cs => cs.Source)
                .WithMany(c => c.ClientSources)
                .HasForeignKey(cs => cs.SourceId);
        }
    }
}
