using Microsoft.AspNetCore.Mvc;
using Serverapi.Repositories;
using System.Threading.Tasks;
using Core.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Cors;
using MongoDB.Bson.IO;

namespace Serverapi.Controllers
{
    [ApiController]
    [Route("api/application")]
    [EnableCors("AllowSpecificOrigin")]
    public class ApplicationController : ControllerBase
    {
        private readonly Iapplicationrepository _appRepository;

        public ApplicationController(Iapplicationrepository applicationRepository)
        {
            _appRepository = applicationRepository;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllApplications()
        {
            var applications = await _appRepository.GetAllApplications();
            return Ok(applications);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddApplication([FromBody] Application application)
        {
            if (application == null)
            {
                return BadRequest("Application cannot be null.");
            }

            await _appRepository.Add(application);
            return CreatedAtAction(nameof(GetAllApplications), new { id = application.Id }, application);
        }

        [HttpPut]
        [Route("update/{appId}")]
        public async Task<IActionResult> UpdateApplication(int appId, [FromBody] Application application)
        {
            if (application == null)
            {
                return BadRequest("Application cannot be null.");
            }

            try
            {
                await _appRepository.UpdateApplication(appId, application);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedApplications()
        {
            var applications = await _appRepository.GetApprovedApplications();
            return Ok(applications);
        }

        [HttpGet("queued")]
        public async Task<IActionResult> GetQueuedApplications()
        {
            var applications = await _appRepository.GetQueuedApplications();
            return Ok(applications);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public void DeleteApplication(int id)
         {
           _appRepository.DeleteApplication(id);
        }

    }
}
