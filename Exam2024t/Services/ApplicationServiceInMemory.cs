using Core.Models;
using System.Diagnostics.Metrics;

namespace Exam2024t.Services
{
    public class ApplicationServiceInMemory : IApplicationService
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

        public Task UpdateApplication(Application application)
        {
            var existingApplication = Applications.FirstOrDefault(a => a.Child.Volunteer.Kræwnr == application.Child.Volunteer.Kræwnr);
            if (existingApplication != null)
            {
                existingApplication.IsApproved = application.IsApproved;
                Console.WriteLine($"Application for {existingApplication.Child.Volunteer.Name} updated to Approved: {existingApplication.IsApproved}");
            }
            else
            {
                Console.WriteLine("No application found to update.");
            }
            return Task.CompletedTask;
        }


        public Task<Application[]> GetQueuedApplications()
        {
            var queuedApps = Applications.Where(a => !a.IsApproved).ToArray();
            Console.WriteLine($"Fetched {queuedApps.Length} Queued Applications");
            return Task.FromResult(queuedApps);
        }

        public Task<Application[]> GetApprovedApplications()
        {
            var approvedApps = Applications.Where(a => a.IsApproved).ToArray();
            Console.WriteLine($"Fetched {approvedApps.Length} Approved Applications");
            return Task.FromResult(approvedApps);
        }

    }
}

