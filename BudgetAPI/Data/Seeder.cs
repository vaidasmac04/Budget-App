using BudgetProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Data
{
    public static class Seeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new BudgetContext(serviceProvider.GetRequiredService<DbContextOptions<BudgetContext>>()))
            {
                if (context.Client.Any())
                {
                    return;   // DB has been seeded
                }

                context.Client.AddRange
                (
                    new Client
                    {
                        Name = "Client1"
                    },

                    new Client
                    {
                        Name = "Client2"
                    }
                );

                context.Income.AddRange
                (
                   new Income
                   {
                       Value = 251.10,
                       Date = new DateTime(2020, 3, 15),
                       ClientId = 1
                   },

                   new Income
                   {
                       Value = 500,
                       Date = new DateTime(2020, 3, 20),
                       ClientId = 1
                   },

                   new Income
                   {
                       Value = 251.10,
                       Date = new DateTime(2020, 3, 15),
                       ClientId = 2
                   },

                   new Income
                   {
                       Value = 199.25,
                       Date = new DateTime(2020, 3, 25),
                       ClientId = 2
                   }
                );

                context.Outcome.AddRange
                (
                   new Outcome
                   {
                       Name = "Pizza",
                       Price = 10.10,
                       Category = "food",
                       Date = new DateTime(2020, 3, 15),
                       ClientId = 1
                   },

                   new Outcome
                   {
                       Name = "Rent",
                       Price = 150,
                       Category = "services",
                       Date = new DateTime(2020, 3, 17),
                       ClientId = 1
                   },

                   new Outcome
                   {
                       Name = "Movie",
                       Price = 5.25,
                       Category = "leisure",
                       Date = new DateTime(2020, 3, 18),
                       ClientId = 2
                   },

                   new Outcome
                   {
                       Name = "Lunch",
                       Price = 4.25,
                       Category = "food",
                       Date = new DateTime(2020, 3, 25),
                       ClientId = 2
                   }
                );


                context.SaveChanges();
            }
        }
    }
}
