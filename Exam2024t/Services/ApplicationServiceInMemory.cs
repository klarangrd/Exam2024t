using Core.Models;
using System.Diagnostics.Metrics;

namespace eksamensprojekttester.Services
{
    public class ApplicationServiceInMemory : IApplicationService
    {
        private static List<Child> Children = new List<Child>() { new Child { ChildName = "Dennis", ChildAge = 13, ClothingSize = "s", Comment = "whatever", Beenbefore = true, Interest = "Golf" } };
        private static List<Volunteer> Volunteers = new List<Volunteer>() { new Volunteer { Name = "Carl", Email = "Carl@gmail.com", Kræwnr = 84282 } };

        private static List<Application> Applications = new List<Application>(){ new Application {IsVolunteer = false, FirstpriorityWeek = "uge 28", FirstpriorityPeriod ="Onsdag - Søndag", SecondpriorityWeek = "Uge 30", SecondpriorityPeriod ="Lørdag - Søndag",  }

        };
            public ApplicationServiceInMemory()
            {
            }

        public Task Add(Child child)
        {
            Task t = new Task(() => Children.Add(child));
            t.Start();
            return t;
        }
        public Task Add(Volunteer volunteer)
        {
            Task t = new Task(() => Volunteers.Add(volunteer));
            t.Start();
            return t;
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
        public Task<Child[]> GetAllChildren()
        {
            Task<Child[]> t = new Task<Child[]>(() => Children.ToArray());
            t.Start();
            return t;
        }
        public Task<Volunteer[]> GetAllVolunteers()
        {
            Task<Volunteer[]> t = new Task<Volunteer[]>(() => Volunteers.ToArray());
            t.Start();
            return t;
        }



    }
    }

