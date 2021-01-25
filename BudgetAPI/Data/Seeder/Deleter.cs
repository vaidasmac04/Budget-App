using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Data.Seeder
{
    public class Deleter : IDeleter
    {
        private BudgetContext _context;

        public Deleter(BudgetContext context)
        {
            _context = context;
        }

        public void DeleteAll()
        {
            _context.RemoveRange(_context.ClientCategories);
            _context.RemoveRange(_context.ClientItems);
            _context.RemoveRange(_context.ClientSources);
            _context.RemoveRange(_context.Incomes);
            _context.RemoveRange(_context.Outcomes);
            _context.RemoveRange(_context.Items);
            _context.RemoveRange(_context.Sources);
            _context.RemoveRange(_context.Categories);
            _context.RemoveRange(_context.Clients);

            _context.SaveChanges();
        }
    }
}
