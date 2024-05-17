using Core.Models;
using System.Threading.Tasks;

namespace Exam2024t.Services
{
    public interface IAdminService
    {
        Task<bool> LoginAdmin(string username, string password);
       
        Task<Admin[]> GetAllAdmin();

        Task<Admin> GetCurrentAdmin();

        Task LogoutAdmin();


    }
}
