using Budget.Domain;
using Budget.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Application.Incomes
{
    public class SourceResolver : ISourceResolver
    {
        private readonly BudgetContext _context;
        private readonly IClientSourceResolver _clientSourceResolver;

        public SourceResolver(BudgetContext context, IClientSourceResolver clientSourceResolver)
        {
            _context = context;
            _clientSourceResolver = clientSourceResolver;
        }

        public async Task<Source> Resolve(string name)
        {
            Source source = await FindSourceByName(name);

            if(source == null)
            {
                source = new Source { Name = name };
            }

            return source;
        }

        private async Task<Source> FindSourceByName(string name)
        {
            return await _context.Sources
                .Where(s => s.Name.ToLower() == name)
                .FirstOrDefaultAsync();
        }
    }
}
