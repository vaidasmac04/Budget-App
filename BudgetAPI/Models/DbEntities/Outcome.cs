using BudgetAPI.Models.DbEntities;
using BudgetProject.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetProject.Models
{
    public class Outcome
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
