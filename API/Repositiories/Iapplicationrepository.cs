using Core;
using Core.Models;
using MongoDB.Bson;

namespace Serverapi.Repositories
{
    public interface Iapplicationrepository
    {
        Task<Application[]> GetAllApplications();

        Task Add(Application application);

        Task UpdateApplication(Application application);

        Task<Application[]> GetApprovedApplications();

        Task<Application[]> GetQueuedApplications();

        void DeleteApplication(int Id);

    }
}
