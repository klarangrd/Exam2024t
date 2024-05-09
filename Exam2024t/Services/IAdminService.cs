using Core.Models;

namespace Exam2024t.Services
{
    public interface IAdminService
    {
        Task<Admin[]> GetAll();

    }
}
