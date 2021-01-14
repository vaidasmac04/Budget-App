using BudgetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Services
{
    public interface IClientAuthentication
    {
        Client Authenticate(string username, string password);
        Client GetById(int id);
        Client Create(Client client, string password);
    }
}
