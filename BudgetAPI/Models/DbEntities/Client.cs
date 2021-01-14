using System.Collections.Generic;


namespace BudgetProject.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual ICollection<Outcome> Outcomes { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
    }
}
