using Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.Metrics;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace Exam2024t.Services
{
    public class ApplicationServiceInMemory //: IApplicationService
    {
        
        private static List<Application> Applications = new List<Application>(){ new Application
    {
        IsVolunteer = false,
        FirstpriorityWeek = "Uge 28",
        FirstpriorityPeriod ="Onsdag-Søndag",
        SecondpriorityWeek = "Uge 30",
        SecondpriorityPeriod ="Lørdag-Søndag",
        Child = new Child
        {
            ChildName = "Dennis",
            ChildAge = 13,
            ClothingSize = "s",
            Comment = "whatever",
            Beenbefore = true,
            Interest = "Golf",
            Volunteer = new Volunteer
            {
                Name = "Carl",
                Email = "Carl@gmail.com",
                Kræwnr = 84282,
            }  } }

        };
            public ApplicationServiceInMemory()
            {
            }

        public Task Add(Application application)
            {
                Task t = new Task(() => Applications.Add( application));
                t.Start();
                return t;
            }

            public Task<Application[]> GetAllApplications()
            {
                Task<Application[]> t = new Task<Application[]>(() => Applications.ToArray());
                t.Start();
                return t;
            }

        public Task UpdateApplication( int id, Application application)
        {
            var existingApplication = Applications.FirstOrDefault(a => a.Id == application.Id);
            if (existingApplication != null)
            {
                existingApplication.FirstpriorityWeek = application.FirstpriorityWeek;
                existingApplication.FirstpriorityPeriod = application.FirstpriorityPeriod;
                existingApplication.IsApproved = application.IsApproved;
                // Other fields to update if necessary
            }
            return Task.CompletedTask;
        }

        public Task<Application[]> GetQueuedApplications()
        {
            var queuedApps = Applications.Where(a => !a.IsApproved).ToArray();
            return Task.FromResult(queuedApps);
        }

        public Task<Application[]> GetApprovedApplications()
        {
            var approvedApps = Applications.Where(a => a.IsApproved).ToArray();
            return Task.FromResult(approvedApps);
        }


        public Task DeleteApplication(ObjectId applicationId)
        {
            var application = Applications.FirstOrDefault(a => a.Id == applicationId);
            if (application != null)
            {
                Applications.Remove(application);
            }
            
            return Task.CompletedTask;
        }
        public async Task DeleteApplication(int Id)
        {
         
        }
    }
}

