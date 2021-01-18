using BudgetAPI.Models.DbEntities;
using System.Collections.Generic;


namespace BudgetProject.Models.DbEntities
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
        public ICollection<ClientCategory> ClientCategories { get; set; }
        public ICollection<ClientItem> ClientItems { get; set; }
        public ICollection<ClientSource> ClientSources { get; set; }
    }
}
