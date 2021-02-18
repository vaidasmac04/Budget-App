
using Budget.Domain;

namespace BudgetAPI.Services
{
    public interface IClientAuthentication
    {
        Client Authenticate(string username, string password);
        Client GetById(int id);
        Client Create(Client client, string password);
    }
}
