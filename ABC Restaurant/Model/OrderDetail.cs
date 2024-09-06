namespace ABC_Restaurant.Model
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TotalPrice{ get; set; }

        public int MenuId { get; set; }
        public Menu Menu { get; set; }

    }
}
