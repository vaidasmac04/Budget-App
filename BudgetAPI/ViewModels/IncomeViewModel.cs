using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.ViewModels
{
    public class IncomeViewModel
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string Date { get; set; }
        public string SourceName { get; set; }
    }
}
