using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Domain
{
    public class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClientSource> ClientSources { get; set; }
    }
}
