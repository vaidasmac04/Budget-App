using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Domain
{
    public class Income
    {
        public int Id { get; set; }
        public double Value { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
    }
}
