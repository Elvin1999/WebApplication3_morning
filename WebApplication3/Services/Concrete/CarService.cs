using WebApplication3.Entities;
using WebApplication3.Repository.Abstract;
using WebApplication3.Services.Abstract;

namespace WebApplication3.Services.Concrete
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepo;

        public CarService(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }

        public Car Add(Car car)
        {
            var result=_carRepo.Add(car);
            _carRepo.SaveChanges();
            return result;
        }

        public bool Delete(Car car)
        {
            _carRepo.Delete(car);
            var result=_carRepo.SaveChanges();
            return result;
        }

        public IQueryable<Car> Get()
        {
            return _carRepo.Get();
        }

        public Car? Get(int id)
        {
            return _carRepo.Get(id);
        }

        public int GetCarAge(Car car)
        {
            var difference = DateTime.Now.Year - car.Year;
            return difference > 0 ? difference : 0;
        }

        public Car Update(Car car)
        {
            var result=_carRepo.Update(car);
            _carRepo.SaveChanges();
            return result;
        }
    }
}
