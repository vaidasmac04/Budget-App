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
        private readonly IMapper _mapper;

        public IncomeUpdater(BudgetContext context, ISourceResolver sourceResolver, IMapper mapper)
        {
            _context = context;
            _sourceResolver = sourceResolver;
            _mapper = mapper;
        }

        public async Task Update(Income income)
        {
            var queriedIncome = await _context.Incomes.FindAsync(income.Id);

            if(queriedIncome == null)
            {
                throw new ArgumentException("Unable to update income because it doesn't exist");
            }

            queriedIncome.SourceId = await _sourceResolver.Resolve(income.Source.Name);
            queriedIncome.Value = income.Value;
            queriedIncome.Date = income.Date;

            await _context.SaveChangesAsync();
        }

    }
}
