using Microsoft.EntityFrameworkCore;

namespace Budget.Persistence.Seeder
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            new Seeder(new CSVParser()).Seed(modelBuilder);
        }
    }
}
