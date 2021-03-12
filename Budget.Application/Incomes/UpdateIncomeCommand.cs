using AutoMapper;
using Budget.Application.Common;
using Budget.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Budget.Application.Incomes
{
    public class UpdateIncomeCommand : IRequest
    {
        public IncomeDTO IncomeDTO { get; set; }
    }

    public class UpdateCommandCommandHandler : IRequestHandler<UpdateIncomeCommand>
    {
        private readonly IMapper _mapper;
        private readonly IIncomeUpdater _updater;
        private readonly IValidator<IncomeDTO> _validator;

        public UpdateCommandCommandHandler(IMapper mapper, IIncomeUpdater updater, 
            IValidator<IncomeDTO> validator)
        {
            _mapper = mapper;
            _updater = updater;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            _validator.Validate(request.IncomeDTO);
            var income = _mapper.Map<Income>(request.IncomeDTO);
            await _updater.Update(income);

            return Unit.Value;
        }
    }



}
