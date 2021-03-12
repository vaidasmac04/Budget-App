using Microsoft.EntityFrameworkCore;
using System;

namespace Budget.Persistence.Seeder
{
    public interface ISeeder
    {
        public void Seed(ModelBuilder modelBuilder);
    }
}
