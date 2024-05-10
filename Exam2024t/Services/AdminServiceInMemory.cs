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
            new Admin { Name="Klara N", Email="klara@example.dk", Username="klara1", Password="klarabanan1" },
        };

        public Task<Admin[]> GetAll()
        {
            Task<Admin[]> t = new Task<Admin[]>(() => Admins.ToArray());
            t.Start();
            return t;
        }

        public Task<bool> LoginAdmin(string username, string password)
        {
            var admin = Admins.FirstOrDefault(a => a.Username == username && a.Password == password);
            return Task.FromResult(admin != null);
        }
    }
}
