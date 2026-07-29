using WebApplication3.Data;
using WebApplication3.Entities;
using WebApplication3.Repository.Abstract;

namespace WebApplication3.Repository.Concrete
{
    public class CarRepository : ICarRepository
    {
        private readonly CarContext _context;

        public CarRepository(CarContext context)
        {
            _context = context;
        }

        public Car Add(Car car)
        {
            var createdCar = _context.Cars.Add(car).Entity;
            return createdCar;
        }

        public void Delete(Car car)
        {
            _context.Cars.Remove(car);
        }

        public IQueryable<Car> Get()
        {
            return _context.Cars;
        }

        public Car? Get(int id)
        {
            return _context.Cars.SingleOrDefault(c => c.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public Car Update(Car car)
        {
            var updatedCar=_context.Cars.Update(car).Entity;
            return updatedCar;
        }
    }
}
