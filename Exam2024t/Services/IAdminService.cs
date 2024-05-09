using Core.Models;

namespace eksamensprojekttester.Services
{
    public interface IAdminService
    {
        Task<Admin[]> GetAll();

    }
}
