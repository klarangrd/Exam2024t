using Core.Models;
using System.Diagnostics.Metrics;

namespace eksamensprojekttester.Services
{ 
        public class AdminServiceInMemory : IAdminService
        {
            private static List<Admin> Admins = new List<Admin>(){ new Admin { Name="klara n", Email="klara@example.dk", Username="klara1", Password="klarabanan1" },

        };

        public AdminServiceInMemory()
        {
        }

        public Task<Admin[]> GetAll()
        {
            Task<Admin[]> t = new Task<Admin[]>(() => Admins.ToArray());
            t.Start();
            return t;
        }

    }
}
