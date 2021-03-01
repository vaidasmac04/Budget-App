using Budget.Application.Interfaces;
using Budget.Domain;
using Budget.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Application.Incomes
{
    public class IncomeAdder : IIncomeAdder
    {
        private readonly BudgetContext _context;
        private readonly ISourceResolver _sourceResolver;
        private readonly IUserAccessor _userAccessor;

        public IncomeAdder(BudgetContext context, ISourceResolver sourceResolver, 
            IUserAccessor userAccessor)
        {
            _context = context;
            _sourceResolver = sourceResolver;
            _userAccessor = userAccessor;
        }

        public async Task Add(Income income)
        {
            income.ClientId = _userAccessor.GetId();
            income.SourceId = await ResolveSource(income.Source.Name);

            //otherwise, on saving changes new source is also added because 
            //income.Source.Id = 0 and it is treated as a new entity
            income.Source = null;

            _context.Add(income);
            await _context.SaveChangesAsync();
        }

        private async Task<int> ResolveSource(string sourceName)
        {
            return await _sourceResolver.Resolve(sourceName);
        }
    }
}
