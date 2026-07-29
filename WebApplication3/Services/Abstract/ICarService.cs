using WebApplication3.Entities;

namespace WebApplication3.Services.Abstract
{
    public interface ICarService
    {
        IQueryable<Car> Get();
        Car? Get(int id);
        bool Delete(Car car);
        Car Update(Car car);
        Car Add(Car car);
        int GetCarAge(Car car);
    }
}
