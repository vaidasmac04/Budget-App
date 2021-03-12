using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Application.Incomes
{
    public class GetIncomesQueryParam
    {
        private string source;
        public string Source { 
            get
            {
                return source;
            }
            set
            {
                source = value.Trim().ToLower();
            }
        }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
