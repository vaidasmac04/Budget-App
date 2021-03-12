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
    public class GetIncomeQuery : IRequest<IncomeDTO>
    {
        public int Id { get; set; }
    }

    public class GetIncomeQueryHandler : IRequestHandler<GetIncomeQuery, IncomeDTO>
    {
        private readonly BudgetContext _context;
        private readonly IMapper _mapper;

        public GetIncomeQueryHandler(BudgetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IncomeDTO> Handle(GetIncomeQuery request, CancellationToken cancellationToken)
        {
            var income = await _context.Incomes
                    .Where(i => i.Id == request.Id)
                    .Include(i => i.Source).SingleOrDefaultAsync();

            return _mapper.Map<IncomeDTO>(income);
        }
    }
}
