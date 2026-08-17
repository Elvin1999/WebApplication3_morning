using WebApplication3.Entities;
using WebApplication3.Models;

namespace WebApplication3.Repository.Abstract
{
    public interface ICarRepository
    {
        Task<List<Car>> Get();
        Task<PagedResult<Car>> GetAll(int page,int pageSize);
        Task<Car?> Get(int id);
        Task Delete(Car car);
        Task<Car> Update(Car car);
        Task<Car> Add(Car car);
        Task<bool> SaveChanges();
    }
}
