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
    public class OfferController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public OfferController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Offer
        [HttpGet]
        [Route("CustomerGet")]
        public ActionResult<IEnumerable<Offer>> GetOffers()
        {
            return _dbContext.Offers
                           .Include(o => o.Resturant)
                           .ToList();
        }

        // GET: api/Offer/{id}
        [HttpGet("CustomerGet/{id}")]
        public ActionResult<Offer> GetOffer(int id)
        {
            var offer = _dbContext.Offers
                                .Include(o => o.Resturant)
                                .FirstOrDefault(o => o.Id == id);

            if (offer == null)
            {
                return NotFound();
            }

            return offer;
        }

        // POST: api/Offer
        [HttpPost]
        [Route("AdminPost")]
        public ActionResult<Offer> PostOffer(Offer offer)
        {
            _dbContext.Offers.Add(offer);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetOffer), new { id = offer.Id }, offer);
        }

        // PUT: api/Offer/{id}
        [HttpPut("AdminUpdate/{id}")]
        public IActionResult UpdateOffer(int id, Offer offer)
        {
            if (id != offer.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(offer).State = EntityState.Modified;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Offers.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Offer/{id}
        [HttpDelete("AdminDelete/{id}")]
        public ActionResult<Offer> DeleteOffer(int id)
        {
            var offer = _dbContext.Offers.Find(id);
            if (offer == null)
            {
                return NotFound();
            }

            _dbContext.Offers.Remove(offer);
            _dbContext.SaveChanges();

            return offer;
        }
    }
}
