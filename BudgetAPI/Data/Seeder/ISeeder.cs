using Microsoft.EntityFrameworkCore;
using System;

namespace BudgetAPI.Data.Seeder
{
    public interface ISeeder
    {
        public void Seed(ModelBuilder modelBuilder);
    }
}
