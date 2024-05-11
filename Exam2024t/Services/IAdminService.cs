using Core.Models;
using System.Threading.Tasks;

namespace Exam2024t.Services
{
    public interface IAdminService
    {
        Task<Admin[]> GetAll();
        Task<bool> LoginAdmin(string username, string password);
    }
}
