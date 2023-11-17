namespace CarDealer.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public decimal Discount { get; set; } = 0.0m;

        public int CarId { get; set; }
        public Car Car { get; set; } = null!;    

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!; 
    }
}
