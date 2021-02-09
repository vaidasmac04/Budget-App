using BudgetAPI.Models;
using BudgetAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Mapping
{
    public class IncomeMapper : IEntityMapper<Income, IncomeDTO>
    {
        public IncomeDTO Map(Income i)
        {
            return new IncomeDTO
            {
                Id = i.Id,
                Value = i.Value,
                Date = i.Date.ToShortDateString(),
                SourceName = i.Source.Name
            };
        }

        public List<IncomeDTO> Map(List<Income> i)
        {
            List<IncomeDTO> incomeViewModels = new List<IncomeDTO>();

            foreach(Income income in i)
            {
                incomeViewModels.Add(new IncomeDTO
                {
                    Id = income.Id,
                    Value = income.Value,
                    Date = income.Date.ToShortDateString(),
                    SourceName = income.Source.Name
                });
            }

            return incomeViewModels;
        }

        public Income MapBack(IncomeDTO o)
        {
            return new Income
            {
                Id = o.Id,
                Value = o.Value,
                Date = DateTime.Parse(o.Date)
            };
        }

        public List<Income> MapBack(List<IncomeDTO> o)
        {
            List<Income> incomes = new List<Income>();

            foreach (IncomeDTO incomeViewModel in o)
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
