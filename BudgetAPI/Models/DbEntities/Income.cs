﻿using BudgetAPI.Models.DbEntities;
using BudgetProject.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetProject.Models
{
    public class Income
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
    }
}
