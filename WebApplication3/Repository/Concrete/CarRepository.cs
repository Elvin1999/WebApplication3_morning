using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Entities;
using WebApplication3.Models;
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

        public async Task<Car> Add(Car car)
        {
            var createdCar = (await _context.Cars.AddAsync(car)).Entity;
            return createdCar;
        }

        public async Task Delete(Car car)
        {
            await Task.Run(() =>
            {
                _context.Cars.Remove(car);
            });
        }

        public Task<List<Car>> Get()
        {
            return _context.Cars.ToListAsync();
        }

        public async Task<Car?> Get(int id)
        {
            return await _context.Cars.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<PagedResult<Car>> GetAll(int page, int pageSize)
        {
            var query = _context.Cars;

            var totalCount = await query.CountAsync();

            var cars=await query
                .OrderBy(x=>x.Id)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Car>
            {
                items = cars,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            };
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Car> Update(Car car)
        {

            var updatedCar = await Task.Run(() =>
            {
                return _context.Cars.Update(car).Entity;
            });
            return updatedCar;
        }
    }
}
