using AutoMapper;
using WebApplication3.Dtos;
using WebApplication3.Entities;
using WebApplication3.Services.Abstract;

namespace WebApplication3.Mappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //CreateMap<Car, CarDto>()
            //    .AfterMap((src, dest) =>
            //    {
            //        var difference = DateTime.Now.Year - src.Year;
            //        dest.CarAge = difference > 0 ? difference : 0;
            //    });
            CreateMap<Car, CarDto>()
                .ForMember(d => d.CarAge,
                opt => opt.MapFrom<CarAgeResolver>());

            CreateMap<CarDto, Car>();
            CreateMap<CarAddDto, Car>();
            CreateMap<CarUpdateDto, Car>();
        }
    }
}
