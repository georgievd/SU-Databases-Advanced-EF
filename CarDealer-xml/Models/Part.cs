using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Part
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; } = 0.0m;

        public int Quantity { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; } = null!;

        public ICollection<PartCar> PartsCars { get; set; } = new List<PartCar>();
    }
}
