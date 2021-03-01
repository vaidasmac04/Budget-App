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

        public async Task<int> Resolve(string name)
        {
            int sourceId = await FindIdByName(name);

            //need to add new source
            if (sourceId == -1)
            {
                var added = _context.Add(new Source { Name = name });
                await _context.SaveChangesAsync();
                sourceId = added.Entity.Id;
            }

            await _clientSourceResolver.Resolve(sourceId);

            return sourceId;
        }

        private async Task<int> FindIdByName(string name)
        {
            if (await _context.Sources.AnyAsync(s => s.Name.ToLower() == name))
            {
                return await _context.Sources
                    .Where(s => s.Name.ToLower() == name)
                    .Select(s => s.Id).FirstAsync();
            }

            return -1;
        }
    }
}
