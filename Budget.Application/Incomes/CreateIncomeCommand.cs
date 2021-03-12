using AutoMapper;
using Budget.Application.Common;
using Budget.Domain;
using Budget.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Budget.Application.Incomes
{
    public class CreateIncomeCommand : IRequest
    {
        public IncomeDTO IncomeDTO { get; set; }
    }

    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<IncomeDTO> _validator;
        private readonly IIncomeAdder _incomeAdder;

        public CreateIncomeCommandHandler(IMapper mapper, 
            IValidator<IncomeDTO> validator, IIncomeAdder incomeAdder)
        {
            _mapper = mapper;
            _validator = validator;
            _incomeAdder = incomeAdder;
        }

        public async Task<Unit> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            _validator.Validate(request.IncomeDTO);

            var income = _mapper.Map<Income>(request.IncomeDTO);

            await _incomeAdder.Add(income);

            return Unit.Value;
        }
    }
}
