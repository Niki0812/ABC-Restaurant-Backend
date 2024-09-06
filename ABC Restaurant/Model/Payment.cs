namespace ABC_Restaurant.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public string Method{ get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
