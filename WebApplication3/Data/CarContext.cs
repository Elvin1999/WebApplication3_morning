using Microsoft.EntityFrameworkCore;
using WebApplication3.Entities;

namespace WebApplication3.Data
{
    public class CarContext:DbContext
    {
        public CarContext(DbContextOptions<CarContext> options)
            : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
    }
}
