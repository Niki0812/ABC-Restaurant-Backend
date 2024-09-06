using System.ComponentModel.DataAnnotations;

namespace ABC_Restaurant.Model
{
    public class Query
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
