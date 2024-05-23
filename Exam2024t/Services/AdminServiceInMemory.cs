using Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam2024t.Services
{
    public class AdminServiceInMemory : IAdminService
    {
        private static List<Admin> Admins = new List<Admin>()
        {
            new Admin { Name="Klara N", Email="klara@example.dk", Username="klara1", Password="banan1" },
            new Admin { Name="Magnus", Email="mag@example.dk", Username="magnus1", Password="magnus1" },
        };

        private Admin _currentAdmin;

        public Task<bool> LoginAdmin(string username, string password)
        {
            var admin = Admins.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (admin != null)
            {
                _currentAdmin = admin;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<Admin> GetCurrentAdmin()
        {
            return Task.FromResult(_currentAdmin);
        }

        public Task LogoutAdmin()
        {
            _currentAdmin = null;
            return Task.CompletedTask;
        }

        public Task<Admin[]> GetAllAdmin()
        {
            return Task.FromResult(Admins.ToArray());
        }
    }
}
