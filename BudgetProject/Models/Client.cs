using System.Collections.Generic;


namespace BudgetProject.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Outcome> Outcomes { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
    }
}
