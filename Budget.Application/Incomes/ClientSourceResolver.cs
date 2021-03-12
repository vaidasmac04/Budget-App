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

        public ClientSourceResolver(BudgetContext context)
        {
            _context = context;
        }

        public async Task<ClientSource> Resolve(int clientId, int sourceId)
        {
            ClientSource clientSource = null;

            if (!await Exists(clientId, sourceId))
            {
                clientSource = new ClientSource
                {
                    ClientId = clientId,
                    SourceId = sourceId
                };
            }

            return clientSource;
        }

        private async Task<bool> Exists(int clientId, int sourceId)
        {
            return await _context.ClientSources
                .AnyAsync(cs => cs.ClientId == clientId && cs.SourceId == sourceId);
        }
    }
}

