using WebApplication3.Entities;

namespace WebApplication3.Repository.Abstract
{
    public interface ICarRepository
    {
        IQueryable<Car> Get();
        Car? Get(int id);
        void Delete(Car car);
        Car Update(Car car);
        Car Add(Car car);
        bool SaveChanges();
    }
}
