using Microsoft.AspNetCore.Mvc;
using Serverapi.Repositories;
using System.Threading.Tasks;
using Core.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Cors;

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
        [Route("update")]
        public async Task<IActionResult> UpdateApplication([FromBody] Application application)
        {
            if (application == null)
            {
                return BadRequest("Application cannot be null.");
            }

            await _appRepository.UpdateApplication(application);
            return NoContent();
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
        public async Task<IActionResult> DeleteApplication(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid Id format.");
            }

            await _appRepository.DeleteApplication(objectId);
            return NoContent();
        }
    }
}
