using BudgetAPI.Models;
using BudgetAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Mapping
{
    public class IncomeMapper : IEntityMapper<Income, IncomeViewModel>
    {
        public IncomeViewModel Map(Income i)
        {
            return new IncomeViewModel
            {
                Id = i.Id,
                Value = i.Value,
                Date = i.Date.ToShortDateString(),
                SourceName = i.Source.Name
            };
        }

        public List<IncomeViewModel> Map(List<Income> i)
        {
            List<IncomeViewModel> incomeViewModels = new List<IncomeViewModel>();

            foreach(Income income in i)
            {
                incomeViewModels.Add(new IncomeViewModel
                {
                    Id = income.Id,
                    Value = income.Value,
                    Date = income.Date.ToShortDateString(),
                    SourceName = income.Source.Name
                });
            }

            return incomeViewModels;
        }

        public Income MapBack(IncomeViewModel o)
        {
            return new Income
            {
                Id = o.Id,
                Value = o.Value,
                Date = DateTime.Parse(o.Date)
            };
        }

        public List<Income> MapBack(List<IncomeViewModel> o)
        {
            List<Income> incomes = new List<Income>();

            foreach (IncomeViewModel incomeViewModel in o)
            {
                incomes.Add(new Income
                {
                    Id = incomeViewModel.Id,
                    Value = incomeViewModel.Value,
                    Date = DateTime.Parse(incomeViewModel.Date)
                });
            }

            return incomes;
        }
    }
}
