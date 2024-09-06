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
    public class QueryController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public QueryController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Query
        [HttpGet]
        [Route("Adminview")]
        public ActionResult<IEnumerable<Query>> GetQueries()
        {
            return _dbContext.Queries.Include(q => q.Customer).ToList();
        }

        // GET: api/Query/{id}
        [HttpGet("AdminView/{id}")]

        public ActionResult<Query> GetQuery(int id)
        {
            var query = _dbContext.Queries.Include(q => q.Customer).FirstOrDefault(q => q.Id == id);

            if (query == null)
            {
                return NotFound();
            }

            return query;
        }

        // POST: api/Query
        [HttpPost]
        [Route("CustomerPost")]


        public ActionResult<Query> PostQuery(Query query)
        {
            _dbContext.Queries.Add(query);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetQuery), new { id = query.Id }, query);
        }



        // DELETE: api/Query/{id}
        [HttpDelete("Admindelete/{id}")]

        public ActionResult<Query> DeleteQuery(int id)
        {
            var query = _dbContext.Queries.Find(id);
            if (query == null)
            {
                return NotFound();
            }

            _dbContext.Queries.Remove(query);
            _dbContext.SaveChanges();

            return query;
        }

        // GET: api/Query/search/{id}
        [HttpGet("search/{id}")]
        public ActionResult<Query> SearchQueryById(int id)
        {
            var query = _dbContext.Queries.Include(q => q.Customer).FirstOrDefault(q => q.Id == id);

            if (query == null)
            {
                return NotFound();
            }

            return query;
        }
    }
}
