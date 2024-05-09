using Core.Models;

namespace eksamensprojekttester.Services
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

