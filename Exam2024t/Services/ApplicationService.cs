using Core.Models;
using MongoDB.Bson;
using System.Net.Http;
using System.Net.Http.Json;

namespace Exam2024t.Services
{
    
        public class ApplicationService : IApplicationService
        {
        private readonly HttpClient http;
        private readonly string serverUrl = "https://localhost:7010/api/application";

        public ApplicationService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<Application[]> GetAllApplications()
        {
            return await http.GetFromJsonAsync<Application[]>($"{serverUrl}/getall");
        }

        public async Task Add(Application application)
        {
            await http.PostAsJsonAsync($"{serverUrl}/add", application);
        }

        public async Task UpdateApplication(Application application)
        {
            await http.PutAsJsonAsync($"{serverUrl}/update", application);
        }

        public async Task<Application[]> GetApprovedApplications()
        {
            return await http.GetFromJsonAsync<Application[]>($"{serverUrl}/approved");
        }

        public async Task<Application[]> GetQueuedApplications()
        {
            return await http.GetFromJsonAsync<Application[]>($"{serverUrl}/queued");
        }

        public async Task DeleteApplication(int Id)
        {
            await http.DeleteAsync($"{serverUrl}/delete/{Id}");
        }


    }
}


