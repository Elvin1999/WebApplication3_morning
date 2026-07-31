using AutoMapper;
using WebApplication3.Dtos;
using WebApplication3.Entities;
using WebApplication3.Services.Abstract;

namespace WebApplication3.Mappers
{
    public class CarAgeResolver : IValueResolver<Car, CarDto, int>
    {
        private readonly ICarService _carService;

        public CarAgeResolver(ICarService carService)
        {
            _carService = carService;
        }

        public int Resolve(Car source, CarDto destination, int destMember, ResolutionContext context)
        {
            return _carService.GetCarAge(source);
        }
    }
}
