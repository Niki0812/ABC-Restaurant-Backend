using ABC_Restaurant.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace busBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly dbContext _dbContext;
        public AdminController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAlladdmin")]
        public async Task<IActionResult> GetAllAdminDetails()
        {

            var admin = await _dbContext.Admins.ToListAsync();


            if (admin == null || admin.Count == 0)
            {
                return NotFound("No Admin not found");
            }

            return Ok(admin);
        }

        [HttpGet]
        [Route("GetAdminByEmail/{email}")]
        public async Task<IActionResult> GetAdminByEmail(string email)
        {
            // Find the admin with the provided email
            var admin = await _dbContext.Admins.Where(a => a.Email == email).ToListAsync();

            // If admin is not found, return 404 Not Found
            if (admin == null)
            {
                return NotFound($"Admin with email '{email}' not found");
            }

            // Return the admin details
            return Ok(admin);
        }


        [HttpPost]
        [Route("Adminlogin")]
        public async Task<IActionResult> AddminLogin(LoginRequest loginRequest)
        {
            // Find the student with the provided email
            var admin = await _dbContext.Admins.FirstOrDefaultAsync(s => s.Email == loginRequest.email);

            // If student is not found or password does not match, return 400 Bad Request
            if (admin == null || admin.Password != loginRequest.password)
            {
                return BadRequest("Invalid email or password");
            }

            // If login is successful, return 200 OK with the student object
            return Ok(admin);
        }













    }
    public class LoginRequest
    {
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
