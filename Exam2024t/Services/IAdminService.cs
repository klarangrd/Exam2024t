using System.Threading.Tasks;
using Core.Models;

namespace Exam2024t.Services
{
    public interface IAdminService
    {
        Task<bool> LoginAdmin(string username, string password);
        Task<Admin> GetCurrentAdmin();
        Task LogoutAdmin();
        Task<Admin[]> GetAllAdmin();

        Task<bool> CheckLogin(string username, string password);
    }
}
