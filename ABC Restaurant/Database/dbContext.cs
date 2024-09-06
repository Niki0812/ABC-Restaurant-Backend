using ABC_Restaurant.Model;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace ABC_Restaurant.Database
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {


        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Resturant> Resturants { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }






    }
}
