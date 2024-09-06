using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ABC_Restaurant.Model;
using ABC_Restaurant.Database;

namespace ABC_Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public PaymentController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Payment
        [HttpGet]
        [Route("AdminGet")]
        public ActionResult<IEnumerable<Payment>> GetPayments()
        {
            return _dbContext.Payments
                           .Include(p => p.Reservation)
                           .ToList();
        }

        // GET: api/Payment/{id}
        [HttpGet("AdminGet/{id}")]
        public ActionResult<Payment> GetPayment(int id)
        {
            var payment = _dbContext.Payments
                                  .Include(p => p.Reservation)
                                  .FirstOrDefault(p => p.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // POST: api/Payment
        [HttpPost]
        [Route("CustomerPay")]
        public ActionResult<Payment> PostPayment(Payment payment)
        {
            _dbContext.Payments.Add(payment);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }
    }
}
