using System.ComponentModel.DataAnnotations;

namespace ABC_Restaurant.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public string Nic { get; set; }
    }
}
