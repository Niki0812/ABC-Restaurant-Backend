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
    public class MenuController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public MenuController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Menu
        [HttpGet]
        [Route("CustomerGet")]
        public ActionResult<IEnumerable<Menu>> GetMenus()
        {
            return _dbContext.Menus
                           .Include(m => m.Resturant)
                           .ToList();
        }

        // GET: api/Menu/{id}
        [HttpGet("CustomerGet/{id}")]
        public ActionResult<Menu> GetMenu(int id)
        {
            var menu = _dbContext.Menus
                               .Include(m => m.Resturant)
                               .FirstOrDefault(m => m.Id == id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        // POST: api/Menu
        [HttpPost]
        [Route("AdminPost")]
        public ActionResult<Menu> PostMenu(Menu menu)
        {
            _dbContext.Menus.Add(menu);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetMenu), new { id = menu.Id }, menu);
        }

        // PUT: api/Menu/{id}
        [HttpPut("AdminUpdate/{id}")]
        public IActionResult UpdateMenu(int id, Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(menu).State = EntityState.Modified;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Menus.Any(e => e.Id == id))
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

        // DELETE: api/Menu/{id}
        [HttpDelete("AdminDelete/{id}")]
        public ActionResult<Menu> DeleteMenu(int id)
        {
            var menu = _dbContext.Menus.Find(id);
            if (menu == null)
            {
                return NotFound();
            }

            _dbContext.Menus.Remove(menu);
            _dbContext.SaveChanges();

            return menu;
        }

        // GET: api/Menu/searchByCategory/{category}
        [HttpGet("searchByCategory/{category}")]
        public ActionResult<IEnumerable<Menu>> SearchByCategory(string category)
        {
            var menus = _dbContext.Menus
                                .Include(m => m.Resturant)
                                .Where(m => m.Category.Contains(category))
                                .ToList();

            if (!menus.Any())
            {
                return NotFound();
            }

            return menus;
        }

        // GET: api/Menu/searchByServiceType/{serviceType}
        [HttpGet("searchByServiceType/{serviceType}")]
        public ActionResult<IEnumerable<Menu>> SearchByServiceType(string serviceType)
        {
            var menus = _dbContext.Menus
                                .Include(m => m.Resturant)
                                .Where(m => m.ServiceType.Contains(serviceType))
                                .ToList();

            if (!menus.Any())
            {
                return NotFound();
            }

            return menus;
        }
    }
}
