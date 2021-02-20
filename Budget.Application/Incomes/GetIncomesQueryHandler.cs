using AutoMapper;
using Budget.Application.Interfaces;
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
    public class GetIncomesQuery : IRequest<List<IncomeDTO>>
    {

    }

    public class GetIncomesQueryHandler : IRequestHandler<GetIncomesQuery, List<IncomeDTO>>
    {
        private readonly BudgetContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public GetIncomesQueryHandler(BudgetContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<List<IncomeDTO>> Handle(GetIncomesQuery request, CancellationToken cancellationToken)
        {
            int id = _userAccessor.GetId();
            var incomes = await _context.Incomes
                .Where(i => i.ClientId == _userAccessor.GetId())
                .Include(i => i.Source)
                .ToListAsync();

            return _mapper.Map<List<IncomeDTO>>(incomes);
        }
    }
}
