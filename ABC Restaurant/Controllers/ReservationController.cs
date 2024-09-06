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
    public class ReservationController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public ReservationController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Reservation
        [HttpGet]
        [Route("AdminView")]
        public ActionResult<IEnumerable<Reservation>> GetReservations()
        {
            return _dbContext.Reservations
                           .Include(r => r.Customer)
                           .Include(r => r.Resturant)
                           .ToList();
        }

        // GET: api/Reservation/{id}
        [HttpGet("AdminView/{id}")]
        public ActionResult<Reservation> GetReservation(int id)
        {
            var reservation = _dbContext.Reservations
                                      .Include(r => r.Customer)
                                      .Include(r => r.Resturant)
                                      .FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // POST: api/Reservation
        [HttpPost]
        [Route("CustomerReservation")]
        public ActionResult<Reservation> PostReservation(Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        

        // DELETE: api/Reservation/{id}
        [HttpDelete("AdminCancel/{id}")]
        public ActionResult<Reservation> DeleteReservation(int id)
        {
            var reservation = _dbContext.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _dbContext.Reservations.Remove(reservation);
            _dbContext.SaveChanges();

            return reservation;
        }

        // GET: api/Reservation/searchByType/{type}
        [HttpGet("searchByType/{type}")]
        public ActionResult<IEnumerable<Reservation>> SearchReservationByType(string type)
        {
            var reservations = _dbContext.Reservations
                                       .Include(r => r.Customer)
                                       .Include(r => r.Resturant)
                                       .Where(r => r.Type.Contains(type))
                                       .ToList();

            if (reservations == null || !reservations.Any())
            {
                return NotFound();
            }

            return reservations;
        }
    }
}
