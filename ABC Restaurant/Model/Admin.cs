using System.ComponentModel.DataAnnotations;

namespace ABC_Restaurant.Model
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
