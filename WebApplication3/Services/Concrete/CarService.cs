using WebApplication3.Entities;
using WebApplication3.Models;
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

        public async Task<Car> Add(Car car)
        {
            var result=await _carRepo.Add(car);
            await _carRepo.SaveChanges();
            return result;
        }

        public async Task<bool> Delete(Car car)
        {
            await _carRepo.Delete(car);
            var result=await _carRepo.SaveChanges();
            return result;
        }

        public async Task<List<Car>> Get()
        {
            return await _carRepo.Get();
        }

        public async Task<Car?> Get(int id)
        {
            return await _carRepo.Get(id);
        }

        public async Task<PagedResult<Car>> GetAll(int page, int pageSize)
        {
            return await _carRepo.GetAll(page, pageSize);
        }

        public int GetCarAge(Car car)
        {
            var difference = DateTime.Now.Year - car.Year;
            return difference > 0 ? difference : 0;
        }

        public async Task<Car> Update(Car car)
        {
            var result= await _carRepo.Update(car);
            await _carRepo.SaveChanges();
            return result;
        }
    }
}
