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
    public class DeleteIncomeCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteIncomeCommandHandler: IRequestHandler<DeleteIncomeCommand>
    {
        private readonly BudgetContext _context;

        public DeleteIncomeCommandHandler(BudgetContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = await _context.Incomes.FindAsync(request.Id);

            if (income == null)
            {
                throw new ArgumentException("Unable to delete income because it doesn't exist");
            }

            _context.Incomes.Remove(income);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }

    }
}
