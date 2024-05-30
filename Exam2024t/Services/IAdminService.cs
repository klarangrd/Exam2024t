using System.Threading.Tasks;
using Core.Models;

namespace Exam2024t.Services
{
    public interface IAdminService
    {
        Task<Admin> GetCurrentAdmin();
        Task LogoutAdmin();
        Task<Admin[]> GetAllAdmin();

        Task<bool> CheckLoginAsync(string username, string password);
    }
}
