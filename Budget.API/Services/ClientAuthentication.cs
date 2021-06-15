using Budget.Domain;
using Budget.Persistence;
using System;
using System.Linq;

namespace Budget.API.Services
{
    public class ClientAuthentication : IClientAuthentication
    {
        private readonly BudgetContext _context;

        public ClientAuthentication(BudgetContext context)
        {
            _context = context;
        }

        public Client Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var client = _context.Clients.SingleOrDefault(x => x.Username == username);

            if (client == null)
                return null;

            if (!VerifyPasswordHash(password, client.PasswordHash, client.PasswordSalt))
                return null;

            return client;
        }

        public Client GetById(int id)
        {
            return _context.Clients.Find(id);
        }

        public Client Create(Client client, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password is required");
            }
               

            if (_context.Clients.Any(x => x.Username == client.Username))
            {
                throw new ArgumentException("Username '" + client.Username + "' is already taken");
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            client.PasswordHash = passwordHash;
            client.PasswordSalt = passwordSalt;

            _context.Clients.Add(client);
            _context.SaveChanges();

            return client;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.");
            }

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
