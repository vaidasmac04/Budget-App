using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Domain
{
    public class ClientSource
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
    }
}
