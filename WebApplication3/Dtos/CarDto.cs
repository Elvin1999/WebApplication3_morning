using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }
        [Required]
        public string? Model { get; set; }
        [Required]
        public string? Vendor { get; set; }
        public double Engine { get; set; }
        public int Year { get; set; }
        public int CarAge { get; set; }
    }
}
