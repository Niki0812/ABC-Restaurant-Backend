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
    public class ReviewController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public ReviewController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Review
        [HttpGet]
        [Route("CustomerView")]
        public ActionResult<IEnumerable<Review>> GetReviews()
        {
            return _dbContext.Reviews
                           .Include(r => r.Customer)
                           .Include(r => r.Resturant)
                           .ToList();
        }

        // GET: api/Review/{id}
        [HttpGet("AdminView/{id}")]
        public ActionResult<Review> GetReview(int id)
        {
            var review = _dbContext.Reviews
                                 .Include(r => r.Customer)
                                 .Include(r => r.Resturant)
                                 .FirstOrDefault(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // POST: api/Review
        [HttpPost]
        [Route("CustomerPost")]
        public ActionResult<Review> PostReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
        }
    }
}
