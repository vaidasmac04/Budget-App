using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Domain
{
    public class ClientItem
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
