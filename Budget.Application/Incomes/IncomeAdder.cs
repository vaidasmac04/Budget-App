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
        private readonly IClientSourceResolver _clientSourceResolver;
        private readonly IUserAccessor _userAccessor;

        public IncomeAdder(BudgetContext context, ISourceResolver sourceResolver, 
            IClientSourceResolver clientSourceReolver, IUserAccessor userAccessor)
        {
            _context = context;
            _sourceResolver = sourceResolver;
            _userAccessor = userAccessor;
            _clientSourceResolver = clientSourceReolver;
        }

        public async Task Add(Income income)
        {
            income.ClientId = _userAccessor.GetId();
            income.Source = await  _sourceResolver.Resolve(income.Source.Name);
           
             _context.Add(income);
            await _context.SaveChangesAsync();

            var clientSource = await _clientSourceResolver.Resolve(income.ClientId, income.Source.Id);
            if (clientSource != null)
            {
                _context.ClientSources.Add(clientSource);
                await _context.SaveChangesAsync();
            }
        }
    }
}
