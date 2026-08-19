using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Dtos;
using WebApplication3.Entities;
using WebApplication3.Models;
using WebApplication3.Services.Abstract;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
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
    public async Task<ActionResult<IEnumerable<CarDto>>> Get()
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
        var carsFromService = await _carService.Get();
        var cars = _mapper.Map<IEnumerable<CarDto>>(carsFromService);

        return Ok(cars);
    }

    [HttpGet("partial")]
    public async Task<ActionResult<PagedResult<Car>>> GetAll(int page=1,int pageSize=10)
    {
      
        var carsFromService = await _carService.GetAll(page,pageSize);
        
        return Ok(carsFromService);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarDto>> Get(int id)
    {
        var car = await _carService.Get(id);
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

    public async Task<ActionResult> Post([FromBody] CarAddDto dto)
    {
        //var car = new Car
        //{
        //    Year = dto.Year,
        //    Model = dto.Model,
        //    Engine = dto.Engine,
        //    Vendor = dto.Vendor,
        //};

        var car = _mapper.Map<Car>(dto);

        var createdCar = await _carService.Add(car);
        return CreatedAtAction(nameof(Get), new
        {
            id = createdCar.Id,
        }, createdCar);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id,[FromBody] CarUpdateDto dto)
    {
        try
        {
            var car=await _carService.Get(id);
            if (car == null) return NotFound();

            //car.Vendor = dto.Vendor;
            //car.Year = dto.Year;
            //car.Model = dto.Model;
            _mapper.Map(dto, car);

            var updatedCar=await _carService.Update(car);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var car = await _carService.Get(id);
        if (car == null)
        {
            return NotFound();
        }

      
        var carDto = await _carService.Delete(car);

        return Ok(carDto);
    }

}
