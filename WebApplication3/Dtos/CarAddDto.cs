using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Dtos
{
    public class CarAddDto
    {
        public string? Model { get; set; }
        [Required]
        public string? Vendor { get; set; }
        public double Engine { get; set; }
        public int Year { get; set; }
    }
}
