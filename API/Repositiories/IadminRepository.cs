using Core;
using Core.Models;
using MongoDB;

namespace Serverapi.Repositories
{
    public interface IadminRepository
    {
        void AddItem(Admin item);
        void DeleteById(int id);
        List<Admin> GetAll();
       // Task<bool> LoginAdmin(string username, string password);
        Task<Admin> GetCurrentAdmin();
        Task LogoutAdmin();
        Task<Admin[]> GetAllAdmin();



        bool CheckLogin(string username, string password);
    }
}
