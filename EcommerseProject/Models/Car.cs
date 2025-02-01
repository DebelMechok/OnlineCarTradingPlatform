using System.ComponentModel.DataAnnotations;

namespace EcommerseProject.Models
{
    public class Car
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Model { get; set; }
        [MaxLength(30)]
        public string Engine { get; set; }

        public decimal Price { get; set; }

        public bool HasAirConditioning { get; set; }

        public int Year { get; set; }

        public string CarPhoto { get; set; }

        public int CategoryId { get; set; }
        //11111
    }
}
