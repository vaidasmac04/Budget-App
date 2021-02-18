using Budget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Services.Incomes
{
    public interface IIncomeHandler
    {
        Task<List<Income>> GetByClientId(int clientId);

        //throws ArgumentException
        Task<List<Income>> GetByClientId(int clientId, string source);

        //throws ArgumentException
        Task<List<Income>> GetByClientId(int clientId, DateTime from, DateTime to);

        //throws ArgumentException
        Task<List<Income>> GetByClientId(int clientId, DateTime from, DateTime to, string source);

        Task<Income> GetById(int id);

        //throws ArgumentException
        Task Add(Income income, int clientId, string sourceName);

        //throws ArgumentException
        Task Delete(int id);

        //throws ArgumentException
        Task Update(Income income, int clientId, string sourceName);
    }
}
