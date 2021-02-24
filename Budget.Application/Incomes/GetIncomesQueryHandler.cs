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
        public GetIncomesQueryParam Param { get; set; }
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
            bool isFilterByDate = request.Param.DateFrom.HasValue && request.Param.DateTo.HasValue;
            
            if (isFilterByDate && request.Param.DateFrom > request.Param.DateTo)
            {
                throw new ArgumentException("End date can't be earlier that start date");
            }

            var incomes = _context.Incomes
            .Where(i => i.ClientId == _userAccessor.GetId())
            .Include(i => i.Source).AsQueryable();

            if (!string.IsNullOrEmpty(request.Param.Source))
            {
                incomes = incomes.Where(i => i.Source.Name == request.Param.Source);
            }

            if(isFilterByDate)
            { 
                incomes = incomes.Where(i => i.Date >= request.Param.DateFrom && i.Date <= request.Param.DateTo);
            }

            return _mapper.Map<List<IncomeDTO>>(await incomes.ToListAsync());
        }
    }
}
