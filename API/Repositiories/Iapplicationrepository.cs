using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serverapi.Repositories
{
    public interface Iapplicationrepository
    {
        Task<Application[]> GetAllApplications();
        Task Add(Application application);
        Task UpdateApplication(int id, Application application);
        Task<Application[]> GetApprovedApplications();
        Task<Application[]> GetQueuedApplications();
        void DeleteApplication(int Id);
        Task<List<string>> GetVolunteerEmails();
        Task<Application> GetApplicationById(int id);//finding the application by its id
    }
}
