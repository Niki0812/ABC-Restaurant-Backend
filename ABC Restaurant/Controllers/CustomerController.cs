using ABC_Restaurant.Database;
using ABC_Restaurant.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace busBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly dbContext _dbContext;
        public CustomerController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        [Route("CustomerRegister")]
        public async Task<IActionResult> AddPassenger(Customer objPassenger)
        {
            try
            {
                // Check if the provided email already exists in the database
                var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(p => p.Email == objPassenger.Email);
                if (existingCustomer != null)
                {
                    return BadRequest("Email already exists"); // Return error message if email already exists
                }

                // Add the new passenger to the database
                _dbContext.Customers.Add(objPassenger);
                await _dbContext.SaveChangesAsync();

                return Ok(objPassenger); // Return the added passenger if successful
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding passenger: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            // Find the student with the provided email
            var Customer = await _dbContext.Customers.FirstOrDefaultAsync(s => s.Email == loginRequest.email);

            // If student is not found or password does not match, return 400 Bad Request
            if (Customer == null || Customer.Password != loginRequest.password)
            {
                return BadRequest("Invalid email or password");
            }

            // If login is successful, return 200 OK with the student object
            return Ok(Customer);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetPassengerById(int id)
        {
            var Customers = await _dbContext.Customers.Where(p => p.Id == id).ToListAsync();

            if (Customers == null)
            {
                return NotFound(); // Return 404 Not Found if passenger with the specified ID is not found
            }

            return Customers;
        }
    }
}
