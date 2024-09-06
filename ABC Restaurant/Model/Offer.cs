namespace ABC_Restaurant.Model
{
    public class Offer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public int ResturantId { get; set; }
        public Resturant Resturant { get; set; }
    }
}
