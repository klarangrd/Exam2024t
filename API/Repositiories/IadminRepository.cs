﻿using Core;
using Core.Models;
using MongoDB;

namespace Serverapi.Repositories
{
    public interface IadminRepository
    {
        void AddItem(Admin item);
        void DeleteById(int id);
        List<Admin> GetAll();
        Task<Admin> GetCurrentAdmin();
        Task LogoutAdmin();
        Task<Admin[]> GetAllAdmin();

        Task<bool> CheckLoginAsync(string username, string password);
    }
}
