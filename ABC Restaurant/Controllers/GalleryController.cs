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
    public class GalleryController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public GalleryController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Gallery
        [HttpGet]
        [Route("CustomerGet")]
        public ActionResult<IEnumerable<Gallery>> GetGalleries()
        {
            return _dbContext.Galleries
                           .Include(g => g.Resturant)
                           .ToList();
        }

        // GET: api/Gallery/{id}
        [HttpGet("CustomerGet/{id}")]
        public ActionResult<Gallery> GetGallery(int id)
        {
            var gallery = _dbContext.Galleries
                                  .Include(g => g.Resturant)
                                  .FirstOrDefault(g => g.Id == id);

            if (gallery == null)
            {
                return NotFound();
            }

            return gallery;
        }

        // POST: api/Gallery
        [HttpPost]
        [Route("AdminPost")]
        public ActionResult<Gallery> PostGallery(Gallery gallery)
        {
            _dbContext.Galleries.Add(gallery);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetGallery), new { id = gallery.Id }, gallery);
        }

        // PUT: api/Gallery/{id}
        [HttpPut("AdminUpdate/{id}")]
        public IActionResult UpdateGallery(int id, Gallery gallery)
        {
            if (id != gallery.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(gallery).State = EntityState.Modified;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Galleries.Any(e => e.Id == id))
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

        // DELETE: api/Gallery/{id}
        [HttpDelete("AdminDelete/{id}")]
        public ActionResult<Gallery> DeleteGallery(int id)
        {
            var gallery = _dbContext.Galleries.Find(id);
            if (gallery == null)
            {
                return NotFound();
            }

            _dbContext.Galleries.Remove(gallery);
            _dbContext.SaveChanges();

            return gallery;
        }
    }
}
