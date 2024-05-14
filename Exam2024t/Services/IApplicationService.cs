using Core.Models;

namespace Exam2024t.Services
{
    public interface IApplicationService
    {
        Task<Application[]> GetAllApplications();

        Task Add(Application application);


    }
}

