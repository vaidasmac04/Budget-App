using BudgetProject.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Models.DbEntities
{
    public class ClientSource
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
    }
}
