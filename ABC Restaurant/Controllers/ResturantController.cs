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
    public class ResturantController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public ResturantController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Resturant
        [HttpGet]
        [Route("CustomerView")]
        public ActionResult<IEnumerable<Resturant>> GetResturants()
        {
            return _dbContext.Resturants.ToList();
        }

        // GET: api/Resturant/{id}
        [HttpGet("AdminView/{id}")]
        public ActionResult<Resturant> GetResturant(int id)
        {
            var resturant = _dbContext.Resturants.Find(id);

            if (resturant == null)
            {
                return NotFound();
            }

            return resturant;
        }

        // POST: api/Resturant
        [HttpPost]
        [Route("AdminPost")]
        public ActionResult<Resturant> PostResturant(Resturant resturant)
        {
            _dbContext.Resturants.Add(resturant);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetResturant), new { id = resturant.Id }, resturant);
        }

        // PUT: api/Resturant/{id}
        [HttpPut("AdminUpdate/{id}")]
        public IActionResult PutResturant(int id, Resturant resturant)
        {
            if (id != resturant.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(resturant).State = EntityState.Modified;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Resturants.Any(e => e.Id == id))
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

        // DELETE: api/Resturant/{id}
        [HttpDelete("AdminDelete/{id}")]
        public ActionResult<Resturant> DeleteResturant(int id)
        {
            var resturant = _dbContext.Resturants.Find(id);
            if (resturant == null)
            {
                return NotFound();
            }

            _dbContext.Resturants.Remove(resturant);
            _dbContext.SaveChanges();

            return resturant;
        }

        // GET: api/Resturant/searchByLocation/{location}
        [HttpGet("searchByLocation/{location}")]
        public ActionResult<IEnumerable<Resturant>> SearchResturantByLocation(string location)
        {
            var resturants = _dbContext.Resturants
                                      .Where(r => r.Location.Contains(location))
                                      .ToList();

            if (resturants == null || !resturants.Any())
            {
                return NotFound();
            }

            return resturants;
        }
    }
}
