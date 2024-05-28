using Microsoft.AspNetCore.Mvc;
using Serverapi.Repositories;
using Core;
using MongoDB;
using Core.Models;


namespace API.Controllers
    {
        [ApiController]
        [Route("api/admins")]
    public class AdminController : ControllerBase
    {
        private readonly IadminRepository mrepo;

        public AdminController(IadminRepository adminRepository)
        {
            mrepo = adminRepository;
        }


        //admin login
        [HttpGet("checklogin")]
        public async Task<IActionResult> CheckLogin([FromQuery] string username, [FromQuery] string password)
        {
            var result = await mrepo.CheckLoginAsync(username, password);
            if (result)
            {
                return Ok(true);
            }
            return Unauthorized();
        }


        [HttpGet("current")]
        public async Task<ActionResult<Admin>> GetCurrentAdmin()
        {
            var admin = await mrepo.GetCurrentAdmin();
            if (admin != null)
            {
                return Ok(admin);
            }
            return NotFound();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await mrepo.LogoutAdmin();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Admin[]>> GetAllAdmin()
        {
            var admins = await mrepo.GetAllAdmin();
            return Ok(admins);
        }
    }
}




