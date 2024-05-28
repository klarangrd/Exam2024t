using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Models;

namespace Exam2024t.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;
        private Admin _currentAdmin;

        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //serviceklasse til at logge ind som admin
        public async Task<bool> CheckLoginAsync(string username, string password)
        {
            var response = await _httpClient.GetAsync($"/api/admins/checklogin?username={username}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                _currentAdmin = new Admin
                {
                    Username = username
                    
                };
                return true;
            }
            return false;
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

        public async Task<Admin[]> GetAllAdmin()
        {
            return await _httpClient.GetFromJsonAsync<Admin[]>("/api/admin");
        }
    }
}
