﻿using BudgetProject.Models;
using BudgetProject.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Models.DbEntities
{
    public class ClientCategory
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
