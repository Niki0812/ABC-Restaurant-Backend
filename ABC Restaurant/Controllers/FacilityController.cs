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
    public class FacilityController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public FacilityController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Facility
        [HttpGet]
        [Route("CustomerGet")]
        public ActionResult<IEnumerable<Facility>> GetFacilities()
        {
            return _dbContext.Facilities
                           .Include(f => f.Resturant)
                           .ToList();
        }

        // GET: api/Facility/{id}
        [HttpGet("{id}")]
        public ActionResult<Facility> GetFacility(int id)
        {
            var facility = _dbContext.Facilities
                                   .Include(f => f.Resturant)
                                   .FirstOrDefault(f => f.Id == id);

            if (facility == null)
            {
                return NotFound();
            }

            return facility;
        }

        // POST: api/Facility
        [HttpPost]
        [Route("AdminPost")]
        public ActionResult<Facility> PostFacility(Facility facility)
        {
            _dbContext.Facilities.Add(facility);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetFacility), new { id = facility.Id }, facility);
        }

        // PUT: api/Facility/{id}
        [HttpPut("AdminUpdate/{id}")]
        public IActionResult PutFacility(int id, Facility facility)
        {
            if (id != facility.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(facility).State = EntityState.Modified;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Facilities.Any(e => e.Id == id))
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

        // DELETE: api/Facility/{id}
        [HttpDelete("AdminDelete/{id}")]
        public ActionResult<Facility> DeleteFacility(int id)
        {
            var facility = _dbContext.Facilities.Find(id);
            if (facility == null)
            {
                return NotFound();
            }

            _dbContext.Facilities.Remove(facility);
            _dbContext.SaveChanges();

            return facility;
        }

        // GET: api/Facility/searchByType/{type}
        [HttpGet("searchByType/{type}")]
        public ActionResult<IEnumerable<Facility>> SearchFacilityByType(string type)
        {
            var facilities = _dbContext.Facilities
                                     .Include(f => f.Resturant)
                                     .Where(f => f.Type.Contains(type))
                                     .ToList();

            if (facilities == null || !facilities.Any())
            {
                return NotFound();
            }

            return facilities;
        }
    }
}
