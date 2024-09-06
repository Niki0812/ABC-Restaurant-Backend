using ABC_Restaurant.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace busBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {

        private readonly dbContext _dbContext;
        public StaffController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllStaff")]
        public async Task<IActionResult> GetAllStaffDetails()
        {

            var staff = await _dbContext.Staffs.ToListAsync();


            if (staff == null || staff.Count == 0)
            {
                return NotFound("No Staff not found");
            }

            return Ok(staff);
        }

        [HttpGet]
        [Route("GetStaffByEmail/{email}")]
        public async Task<IActionResult> GetStaffByEmail(string email)
        {
            // Find the admin with the provided email
            var staff = await _dbContext.Staffs.Where(a => a.Email == email).ToListAsync();

            // If admin is not found, return 404 Not Found
            if (staff == null)
            {
                return NotFound($"Staff with email '{email}' not found");
            }

            // Return the admin details
            return Ok(staff);
        }


        [HttpPost]
        [Route("Stafflogin")]
        public async Task<IActionResult> StaffLogin(LoginRequest loginRequest)
        {
            // Find the student with the provided email
            var staff = await _dbContext.Staffs.FirstOrDefaultAsync(s => s.Email == loginRequest.email);

            // If student is not found or password does not match, return 400 Bad Request
            if (staff == null || staff.Password != loginRequest.password)
            {
                return BadRequest("Invalid email or password");
            }

            // If login is successful, return 200 OK with the student object
            return Ok(staff);
        }













    }
    public class LoginRequest1
    {
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
