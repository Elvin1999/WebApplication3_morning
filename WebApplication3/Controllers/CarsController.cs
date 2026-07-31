using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entities;
using WebApplication3.Data;
using WebApplication3.Services.Abstract;
using WebApplication3.Dtos;
using AutoMapper;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IMapper _mapper;

    public CarsController(ICarService carService, IMapper mapper)
    {
        _carService = carService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CarDto>> Get()
    {
        //var cars = _carService.Get()
        //    .Select(c => new CarDto
        //    {
        //        Id = c.Id,
        //        Model = c.Model,
        //        Vendor = c.Vendor,
        //        Engine = c.Engine,
        //        Year = c.Year,
        //        CarAge = _carService.GetCarAge(c)
        //    });
        var cars = _mapper.Map<IEnumerable<CarDto>>(_carService.Get());

        return Ok(cars);
    }

    [HttpGet("{id:int}")]
    public ActionResult<CarDto> Get(int id)
    {
        var car = _carService.Get(id);
        if (car == null)
        {
            return NotFound();
        }
        //var carDto = new CarDto
        //{
        //    Id = car.Id,
        //    Model = car.Model,
        //    Vendor = car.Vendor,
        //    Engine = car.Engine,
        //    Year = car.Year,
        //    CarAge = _carService.GetCarAge(car)
        //};
        var carDto=_mapper.Map<CarDto>(car);

        return Ok(carDto);
    }

    [HttpPost]
    public ActionResult Post([FromBody] CarAddDto dto)
    {
        //var car = new Car
        //{
        //    Year = dto.Year,
        //    Model = dto.Model,
        //    Engine = dto.Engine,
        //    Vendor = dto.Vendor,
        //};

        var car = _mapper.Map<Car>(dto);

        var createdCar = _carService.Add(car);
        return CreatedAtAction(nameof(Get), new
        {
            id = createdCar.Id,
        }, createdCar);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id,[FromBody] CarUpdateDto dto)
    {
        try
        {
            var car=_carService.Get(id);
            if (car == null) return NotFound();

            //car.Vendor = dto.Vendor;
            //car.Year = dto.Year;
            //car.Model = dto.Model;
            _mapper.Map(dto, car);

            var updatedCar=_carService.Update(car);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
