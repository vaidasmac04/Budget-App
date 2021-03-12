using Budget.Domain;
using Microsoft.EntityFrameworkCore;

namespace Budget.Persistence.Seeder
{
    public class Seeder : ISeeder
    {
        private IParser _parser;

        public Seeder(IParser parser)
        {
            _parser = parser;      
        }
        public void Seed(ModelBuilder modelBuilder)
        {
            var clients = _parser.Parse<Client>(CSVFiles.CLIENTS);
            modelBuilder.Entity<Client>().HasData(clients);

            var categories = _parser.Parse<Category>(CSVFiles.CATEGORIES);
            modelBuilder.Entity<Category>().HasData(categories);

            var items = _parser.Parse<Item>(CSVFiles.ITEMS);
            modelBuilder.Entity<Item>().HasData(items);

            var sources = _parser.Parse<Source>(CSVFiles.SOURCES);
            modelBuilder.Entity<Source>().HasData(sources);

            var clientCategories = _parser.Parse<ClientCategory>(CSVFiles.CLIENT_CATEGORIES);
            modelBuilder.Entity<ClientCategory>().HasData(clientCategories);

            var clientSources = _parser.Parse<ClientSource>(CSVFiles.CLIENT_SOURCES);
            modelBuilder.Entity<ClientSource>().HasData(clientSources);

            var clientItems = _parser.Parse<ClientItem>(CSVFiles.CLIENT_ITEMS);
            modelBuilder.Entity<ClientItem>().HasData(clientItems);

            var incomes = _parser.Parse<Income>(CSVFiles.INCOMES);
            modelBuilder.Entity<Income>().HasData(incomes);

            var outcomes = _parser.Parse<Outcome>(CSVFiles.OUTCOMES);
            modelBuilder.Entity<Outcome>().HasData(outcomes);

        }
    }
}
