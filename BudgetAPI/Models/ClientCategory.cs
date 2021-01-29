using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Models
{
    public class ClientCategory
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
