using Budget.Domain;
using Budget.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Services.Incomes
{
    public class IncomeHandler : IIncomeHandler
    {
        private readonly BudgetContext _context;
        public IncomeHandler(BudgetContext context)
        {
            _context = context;
        }

        public async Task<List<Income>> GetByClientId(int clientId)
        {
            return await _context.Incomes.Where(i => i.ClientId == clientId)
                .Include(i => i.Source)
                .ToListAsync();
        }

        public async Task<List<Income>> GetByClientId(int clientId, string source)
        {
            source = source.Trim().ToLower();
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentException("Source is required");
            }

            return await _context.Incomes.Where(i => i.ClientId == clientId)
               .Where(i => i.Source.Name.ToLower() == source)
               .Include(i => i.Source)
               .ToListAsync();
        }

        public async Task<List<Income>> GetByClientId(int clientId, DateTime from, DateTime to)
        {
            if(from > to)
            {
                throw new ArgumentException("End date can't be less than start date");
            }

            return await _context.Incomes.Where(i => i.ClientId == clientId)
               .Where(i => i.Date >= from && i.Date <= to)
               .Include(i => i.Source)
               .ToListAsync();
        }

        public async Task<List<Income>> GetByClientId(int clientId, DateTime from, DateTime to, string source)
        {
            source = source.Trim().ToLower();
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentException("Source is required");
            }

            if (from > to)
            {
                throw new ArgumentException("End date can't be less than start date");
            }

            return await _context.Incomes.Where(i => i.ClientId == clientId)
               .Where(i => i.Date >= from && i.Date <= to)
               .Where(i => i.Source.Name == source)
               .Include(i => i.Source)
               .ToListAsync();
        }

        public async Task<Income> GetById(int id)
        {
            return await _context.Incomes.SingleAsync(i => i.Id == id);
        }

        public async Task Update(Income income, int clientId, string sourceName)
        {
            income.ClientId = clientId;

            sourceName = sourceName.Trim().ToLower();
            int sourceId = await FindSourceIdByName(sourceName);

            //need to add new source
            if (sourceId == -1)
            {
                var added = _context.Add(new Source { Name = sourceName });
                await _context.SaveChangesAsync();
                sourceId = added.Entity.Id;
            }

            income.SourceId = sourceId;
            await AddClientSource(income.ClientId, income.SourceId);

            _context.Entry(income).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Add(Income income, int clientId, string sourceName)
        {
            income.ClientId = clientId;

            if (income.Value <= 0)
            {
                throw new ArgumentException("Income value must be greater than 0");
            }
            
            if (!_context.Clients.Any(c => c.Id == income.ClientId))
            {
                throw new ArgumentException("Client not found");
            }

            if (string.IsNullOrEmpty(sourceName))
            {
                throw new ArgumentException("Source is required");
            }

            sourceName = sourceName.Trim().ToLower();
            int sourceId = await FindSourceIdByName(sourceName);

            //need to add new source
            if(sourceId == -1)
            {
                var added = _context.Add(new Source { Name = sourceName });
                await _context.SaveChangesAsync();
                sourceId = added.Entity.Id;
            }

            income.SourceId = sourceId;

            //check if new client source is needed to add
            await AddClientSource(income.ClientId, income.SourceId);

            _context.Add(income);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var income = await _context.Incomes.FindAsync(id);

            if (income == null)
            {
                throw new ArgumentException("Income was not found when trying to delete");
            }

            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();
        }

        private async Task<int> FindSourceIdByName(string name)
        {
            if (await _context.Sources.AnyAsync(s => s.Name.ToLower() == name)){
                return await _context.Sources
                    .Where(s => s.Name.ToLower() == name)
                    .Select(s => s.Id).FirstAsync();
            }

            return -1;
        }

        private async Task<bool> ClientSourceExists(int clientId, int sourceId)
        {
            return await _context.ClientSources
                .AnyAsync(cs => cs.ClientId == clientId && cs.SourceId == sourceId);
        }

        private async Task AddClientSource(int clientId, int sourceId)
        {
            if (!await ClientSourceExists(clientId, sourceId))
            {
                _context.Add(new ClientSource
                {
                    ClientId = clientId,
                    SourceId = sourceId
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
