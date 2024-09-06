namespace ABC_Restaurant.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Tablenumber { get; set; }
        public DateTime Date { get; set; }
        public string NumberofSeats { get; set; }
        public string Type { get; set; }
        public string Status{ get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ResturantId { get; set; }
        public Resturant Resturant { get; set; }
    }
}
