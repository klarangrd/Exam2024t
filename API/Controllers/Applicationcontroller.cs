using Microsoft.AspNetCore.Mvc;
using Core;
using MongoDB;
using Serverapi.repositories;
using Serverapi.Repositories;
using Core.Models;

namespace API.Controllers
{


    [ApiController]
    [Route("api/applications")]
    public class Applicationcontroller : ControllerBase
    {
        private Iapplycationrepository mRepo;

        public Applicationcontroller(Iapplycationrepository repo)
        {
            mRepo = repo;
        }

        [HttpGet]
        [Route("getall")]
        public IEnumerable<Application> GetAll()
        {
            return mRepo.GetAll();
        }

        [HttpPost]
        [Route("add")]
        public void AddItem(Application item)
        {
            mRepo.AddItem(item);
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public void DeleteItem(int id)
        {
            mRepo.DeleteById(id);
        }

        /*
        [HttpPut]
        [Route("update")]
        public void UpdateItem(Application product)
        {
            mRepo.UpdateItem(product);
        }
        */

    }
}





