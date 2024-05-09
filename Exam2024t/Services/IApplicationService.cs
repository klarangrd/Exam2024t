using Core.Models;

namespace Exam2024t.Services
{
    public interface IApplicationService
    {
        Task<Application[]> GetAllApplications();
        Task<Child[]> GetAllChildren();

        Task<Volunteer[]> GetAllVolunteers();

        Task Add(Application application);

        Task Add(Child child);

        Task Add(Volunteer volunteer);

    }
}

