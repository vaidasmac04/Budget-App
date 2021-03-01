using Budget.Domain;
using Budget.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Application.Incomes
{
    public class IncomeDTOValidator : IValidator<IncomeDTO>
    {
        public void Validate(IncomeDTO incomeDTO)
        {
            if(incomeDTO.Value <= 0)
            {
                throw new ArgumentException("Value must be greater than 0");
            }

            if (string.IsNullOrEmpty(incomeDTO.SourceName))
            {
                throw new ArgumentException("Source name is required");
            }
        }
    }
}
