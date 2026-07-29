using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Dtos
{
    public class CarUpdateDto
    {
        public string? Model { get; set; }
        [Required]
        public string? Vendor { get; set; }
        public int Year { get; set; }
    }
}
