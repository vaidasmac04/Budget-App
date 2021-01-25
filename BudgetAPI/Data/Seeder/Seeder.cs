using BudgetAPI.Models.DbEntities;
using BudgetProject.Models;
using BudgetProject.Models.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetAPI.Data.Seeder
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
            Parser parser = new Parser(_parser);

            var clients = parser.Parse<Client>(CSVFiles.CLIENTS);
            modelBuilder.Entity<Client>().HasData(clients);

            var categories = parser.Parse<Category>(CSVFiles.CATEGORIES);
            modelBuilder.Entity<Category>().HasData(categories);

            var items = parser.Parse<Item>(CSVFiles.ITEMS);
            modelBuilder.Entity<Item>().HasData(items);

            var sources = parser.Parse<Source>(CSVFiles.SOURCES);
            modelBuilder.Entity<Source>().HasData(sources);

            var clientCategories = parser.Parse<ClientCategory>(CSVFiles.CLIENT_CATEGORIES);
            modelBuilder.Entity<ClientCategory>().HasData(clientCategories);

            var clientSources = parser.Parse<ClientSource>(CSVFiles.CLIENT_SOURCES);
            modelBuilder.Entity<ClientSource>().HasData(clientSources);

            var clientItems = parser.Parse<ClientItem>(CSVFiles.CLIENT_ITEMS);
            modelBuilder.Entity<ClientItem>().HasData(clientItems);

            var incomes = parser.Parse<Income>(CSVFiles.INCOMES);
            modelBuilder.Entity<Income>().HasData(incomes);

            var outcomes = parser.Parse<Outcome>(CSVFiles.OUTCOMES);
            modelBuilder.Entity<Outcome>().HasData(outcomes);

        }
    }
}
