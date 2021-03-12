using AutoMapper;
using Budget.Application.Interfaces;
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
    public class IncomeUpdater : IIncomeUpdater
    {
        private readonly BudgetContext _context;
        private readonly ISourceResolver _sourceResolver;
        private readonly IClientSourceResolver _clientSourceResolver;
        private readonly IMapper _mapper;

        public IncomeUpdater(BudgetContext context, ISourceResolver sourceResolver, 
            IClientSourceResolver clientSourceResolver, IMapper mapper)
        {
            _context = context;
            _sourceResolver = sourceResolver;
            _mapper = mapper;
            _clientSourceResolver = clientSourceResolver;
        }

        public async Task Update(Income income)
        {
            var queriedIncome = await _context.Incomes.FindAsync(income.Id);

            if(queriedIncome == null)
            {
                throw new ArgumentException("Unable to update income because it doesn't exist");
            }

            queriedIncome.Source = await _sourceResolver.Resolve(income.Source.Name);
            queriedIncome.Value = income.Value;
            queriedIncome.Date = income.Date;
            await _context.SaveChangesAsync();

            var clientSource = await _clientSourceResolver.Resolve(queriedIncome.ClientId, queriedIncome.Source.Id);
            if (clientSource != null)
            {
                _context.ClientSources.Add(clientSource);
                await _context.SaveChangesAsync();
            }
        }

    }
}
