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
    public class ClientSourceResolver : IClientSourceResolver
    {
        private readonly BudgetContext _context;
        private readonly IUserAccessor _userAccessor;

        public ClientSourceResolver(BudgetContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task Resolve(int sourceId)
        {
            int clientId = _userAccessor.GetId();

            if (!await Exists(clientId, sourceId))
            {
                _context.Add(new ClientSource
                {
                    ClientId = clientId,
                    SourceId = sourceId
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task<bool> Exists(int clientId, int sourceId)
        {
            return await _context.ClientSources
                .AnyAsync(cs => cs.ClientId == clientId && cs.SourceId == sourceId);
        }
    }
}
