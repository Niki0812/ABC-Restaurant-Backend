namespace ABC_Restaurant.Model
{
    public class Facility
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int ResturantId { get; set; }
        public Resturant Resturant { get; set; }
    }
}
