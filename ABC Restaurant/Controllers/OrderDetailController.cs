using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ABC_Restaurant.Model;
using ABC_Restaurant.Database;
using System.Linq;
using System.Collections.Generic;

namespace ABC_Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public OrderDetailsController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/OrderDetails
        [HttpPost]
        [Route("CustomerPost")]
        public IActionResult CreateOrderDetail([FromBody] OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                return BadRequest("OrderDetail object cannot be null.");
            }

            _dbContext.OrderDetails.Add(orderDetail);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetAllOrderDetails), new { id = orderDetail.Id }, orderDetail);
        }

        // GET: api/OrderDetails
        [HttpGet]
        [Route("AdminGet")]
        public IActionResult GetAllOrderDetails()
        {
            var orderDetails = _dbContext.OrderDetails
                                       .Include(od => od.Menu)
                                       .ToList();

            if (orderDetails == null || orderDetails.Count == 0)
            {
                return NotFound("No order details found.");
            }

            return Ok(orderDetails);
        }

        // DELETE: api/OrderDetails/{id}
        [HttpDelete("AdminDelete/{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            var orderDetail = _dbContext.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return NotFound("OrderDetail not found.");
            }

            _dbContext.OrderDetails.Remove(orderDetail);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
