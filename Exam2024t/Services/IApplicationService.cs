﻿using Core.Models;
using MongoDB.Bson;

namespace Exam2024t.Services
{
    public interface IApplicationService
    {
        Task<Application[]> GetAllApplications();

        Task Add(Application application);

        Task UpdateApplication(int id, Application application);

        public Task<Application[]> GetApprovedApplications();

        Task<Application[]> GetQueuedApplications();

        Task DeleteApplication(int Id);

        Task<List<string>> GetVolunteerEmails();


    }
}

