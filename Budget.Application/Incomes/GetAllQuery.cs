using AutoMapper;
using Budget.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Budget.Application.Incomes
{
    public class GetAllQuery
    {
        public class Query : IRequest<List<IncomeDTO>>
        {
            public int ClientId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<IncomeDTO>>
        {
            private readonly BudgetContext _context;
            private readonly IMapper _mapper;

            public Handler(BudgetContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<IncomeDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var incomes = await _context.Incomes
                    .Where(i => i.ClientId == request.ClientId)
                    .Include(i => i.Source)
                    .ToListAsync();

                return _mapper.Map<List<IncomeDTO>>(incomes);
            }
        }
    }
}
