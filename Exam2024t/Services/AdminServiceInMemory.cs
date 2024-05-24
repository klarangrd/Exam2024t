using Core.Models;
using Exam2024t.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exam2024t.Services
{
    public class AdminServiceInMemory : IAdminService
    {
        private static List<Core.Models.Admin> Admins = new List<Core.Models.Admin>()
        {
            /*
            new Admin { Name="Klara N", Email="klara@example.dk", Username="klara1", Password="klarabanan1" },
            new Admin { Name="Magnus", Email="mag@example.dk", Username="magnus1", Password="magnus1" },
            */
        };

        private Core.Models.Admin _currentAdmin;

        

        public Task<Core.Models.Admin> GetCurrentAdmin()
        {
            return Task.FromResult(_currentAdmin);
        }

        public Task LogoutAdmin()
        {
            _currentAdmin = null;
            return Task.CompletedTask;
        }

        public Task<Core.Models.Admin[]> GetAllAdmin()
        {
            return Task.FromResult(Admins.ToArray());
        }


        public async Task<bool> CheckLoginAsync(string username, string password)
        {
            return true;
        }
    }
}
